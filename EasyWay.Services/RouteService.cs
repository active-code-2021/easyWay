using EasyWay.Core;
using EasyWay.Core.Entities;
using EasyWay.Data;
using Google.OrTools.ConstraintSolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Services
{

    public class RouteService
    {
     
        private OrderRepository _OrderRepository;
        private IDistanceMatrixSettings _settings;
        private DeliveryManRepository _deliveryManRepository;
        //public CustomerRepository _customerRepository;

        public RouteService(OrderRepository orderRepository, IDistanceMatrixSettings settings, DeliveryManRepository deliveryManRepository)
        {
            _OrderRepository = orderRepository;
            _settings = settings;
            _deliveryManRepository = deliveryManRepository;
        }

       // public async Task MatrixAsync()
       public async Task<List<string>> CalculateRoutes()
        {//הזמנות שלא בוצעו
            var orders = _OrderRepository.DoneOrNot();
            List<string> readyRoutelst = null;
            // TODO add warehouse to orders [0] before  calcaulating the distance matrix;
            var warehuose = _OrderRepository.getWarehouse();
            //List < DistanceMatrix > _distanceMatrixlst = null;
            orders.Insert(0, warehuose);
            int count = (orders.Count / 10)+1;

            //for (int i = 0; i <= count; i++)
            //{
                var distanceMatrix = await CreateDistanceMatrix(orders);
               // _distanceMatrixlst.Add(distanceMatrix);

                //max value element
                // Create Routing Index Manager
                RoutingIndexManager manager =
                    new RoutingIndexManager(distanceMatrix.origin_addresses.Count, 3, 0); // TODO: don't force to return to warehouse

                // Create Routing Model.
                RoutingModel routing = new RoutingModel(manager);

                // Create and register a transit callback.
                int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
                {
                    // Convert from routing variable Index to distance matrix NodeIndex.
                    var fromNode = manager.IndexToNode(fromIndex);
                    var toNode = manager.IndexToNode(toIndex);
                    return distanceMatrix.rows[fromNode].elements[toNode].distance.value;
                });

                // Define cost of each arc.
                routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

                var maxValueDistance = distanceMatrix.rows.Max(r => r.elements.Sum(e => e.distance.value));

                // Add Distance constraint.
                routing.AddDimension(transitCallbackIndex, 0, maxValueDistance / 4, // TODO: calc max distance 
                                     true, // start cumul to zero
                                     "Distance");
                RoutingDimension distanceDimension = routing.GetMutableDimension("Distance");
                //distanceDimension.SetGlobalSpanCostCoefficient(100000);

                // Setting first solution heuristic.
                RoutingSearchParameters searchParameters =
                    operations_research_constraint_solver.DefaultRoutingSearchParameters();
                searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

                // Solve the problem.
                Assignment solution = routing.SolveWithParameters(searchParameters);
                return PrintSolution(distanceMatrix, routing, manager, solution,orders);
           // }
            //return readyRoutelst;
        }

        private async Task<DistanceMatrix> CreateDistanceMatrix(List<Order> orders)
        {
            //  רשימת הכתובות
            var addresses = string.Join('|', orders.Select(o => $"{o.Address.Coordinates.Latitude},{o.Address.Coordinates.Longitude}"));

            //TODO: move to app settings
            var apiKey = "AIzaSyBFVQTB-gOzy3rhID9yuz8ejN_QL70qCqQ"; 
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={addresses}&destinations={addresses}&key={apiKey}";
          
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<DistanceMatrix>(responseBody);
            }
        }
        public List<string> PrintSolution(in DistanceMatrix data, in RoutingModel routing, in RoutingIndexManager manager,
                            in Assignment solution,List<Order>orders)
        {
            var sb = new StringBuilder($"Objective {solution.ObjectiveValue()}:");
            
            // Inspect solution.
             long maxRouteDistance = 0;
            List<string> route = new List<string>();
            for (int i = 0; i <3; ++i)
            {
                // File.AppendAllText("log.txt", "Route for Vehicle {0}:", i);
                var id = _deliveryManRepository.GetId();
                long routeDistance = 0;
                var index = routing.Start(i);
                while (routing.IsEnd(index) == false)
                {
                    sb.AppendLine($"{ manager.IndexToNode((int)index)}");
                    var previousIndex = index;
                    index = solution.Value(routing.NextVar(index));
                    routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
                    var node = manager.IndexToNode((int)index);
                    route.Add(data.origin_addresses[node]);
                    orders[node].SetDeliverymanId(id);
                   
                    _OrderRepository.Update(orders[node].Id, orders[node]);
                }
                sb.AppendLine($"{manager.IndexToNode((int)index)}");
                sb.AppendLine($"Distance of the route: {routeDistance}m");
                maxRouteDistance = Math.Max(routeDistance, maxRouteDistance);
                route.Add("0");
            }
            sb.AppendLine($"Maximum distance of the routes: {maxRouteDistance}m");

            File.WriteAllText("log.txt", sb.ToString());

            return route;

        }
    }
}

