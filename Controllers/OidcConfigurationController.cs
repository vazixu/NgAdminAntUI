﻿using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NgAdminAntUI.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> logger;

        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider, ILogger<OidcConfigurationController> _logger)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
            logger = _logger;
        }

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            logger.LogInformation("API _configuration 调用 {ClientId}", clientId);

            var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            
            return Ok(parameters);
        }
    }
}
