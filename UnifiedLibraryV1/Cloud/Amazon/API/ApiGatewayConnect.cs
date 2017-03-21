using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace UnifiedLibraryV1.Cloud.Amazon.API {
    public class ApiGatewayConnect : Common {
        public bool TestConnect(){
            /*
            AmazonLambdaClient lambdaClient = new AmazonLambdaClient();
            InvokeResponse ir = lambdaClient.Invoke(new InvokeRequest());

            String JsonResponse = Encoding.UTF8.GetString( ir.Payload.ToArray() );
            */        
            return false;
        }
    }
}
