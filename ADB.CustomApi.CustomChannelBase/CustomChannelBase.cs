using ADB.CustomApi.CustomChannelBase.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.CustomApi.CustomChannelBase
{
    public class CustomChannelBase : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var tracingService = serviceProvider.Get<ITracingService>();
           
            var pluginExecutionContext = serviceProvider.Get<IPluginExecutionContext>();

            // "payload" attribute is required by contract
            var payload = pluginExecutionContext.InputParameters["payload"] as string;
            tracingService.Trace(payload);

            var organizationService = serviceProvider.Get<IOrganizationServiceFactory>().CreateOrganizationService(null);

            var payloadObject = JsonUtils.Deserialize<Payload>(payload);
            bool condition = true;
            
            
            //TODO - Logica de Custom channel

            var responseObject = new Response()
            {
                ChannelDefinitionId = payloadObject.ChannelDefinitionId,
                MessageId = new Guid().ToString(),
                RequestId = payloadObject.RequestId,
                // Translate provider statuses to Marketing statuses
                Status = condition ? "Sent" : "SendingFailed",
                StatusDetails = null
            };

            // "response" attribute is required by contract
            pluginExecutionContext.OutputParameters["response"] = JsonUtils.Serialize(responseObject);
        }
    }
}
