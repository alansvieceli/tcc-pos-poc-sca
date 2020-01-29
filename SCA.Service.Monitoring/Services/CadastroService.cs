using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCA.Service.Monitoring.Data;
using SCA.Shared.Entities.Alert;
using Microsoft.EntityFrameworkCore;
using SCA.Shared.Entities.Enums;
using SCA.Shared.Entities.Monitoring;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace SCA.Service.Monitoring.Services
{
    public class CadastroService
    {
        private readonly ILogger _logger;
        private readonly MonitoramentoContext _context;
        private readonly string host;
        private readonly int port;
        private readonly HttpClient _clientHttp;


        public CadastroService(ILogger<CadastroService> logger, IConfiguration config, MonitoramentoContext context)
        {
            this._logger = logger;
            this._context = context;

            this._clientHttp = new HttpClient();

            this.host = config.GetSection("ConfigApp").GetSection("host").Value;
            this.port = ConfigurationBinder.GetValue<int>(config.GetSection("ConfigApp"), "port", 80);
        }

        public async Task<Cadastro> FindByIdAsync(int? id)
        {
            return (id == null) ?
                null :
                await _context.Cadastro
                        .Include(b => b.Regiao)
                        .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task InsertAsync(Cadastro cadastro)
        {
            _context.Add(cadastro);
            await _context.SaveChangesAsync();
        }

        public async Task EnviarAlertas(SensorStatus status, int regiaoId)
        {
            if ((status == SensorStatus.Vermelho) || (status == SensorStatus.Preto))
            {
                string url = $"http://alert:80/api/send/{regiaoId}";
                this._logger.LogInformation($"##### Enviando Mensagens ({regiaoId}) {url} #####");
                HttpResponseMessage response = await _clientHttp.PostAsync(url, null);
                this._logger.LogDebug($"### {url} > {response.StatusCode} ###");
            } else
            {
                this._logger.LogInformation($"##### Não Enviar Mensagens ({regiaoId}) #####");
            }
        }
    }
}
