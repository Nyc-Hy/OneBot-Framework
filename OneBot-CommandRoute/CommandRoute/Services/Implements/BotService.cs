using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OneBot.CommandRoute.Models.VO;

using Sora;
using Sora.Interfaces;
using Sora.Net;
using Sora.Net.Config;
using Sora.OnebotModel;
using YukariToolBox.LightLog;

namespace OneBot.CommandRoute.Services.Implements
{
    /// <summary>
    /// CQHTTP 客户端（Sora）
    /// </summary>
    public class BotService : IBotService
    {
        /// <summary>
        /// Sora WS 服务
        /// </summary>
        public ISoraService SoraService { get; private set; }

        /// <summary>
        /// Sora WS 服务设置
        /// </summary>
        public ISoraConfig ServiceConfig { get; private set; }

        /// <summary>
        /// 依赖注入服务
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// OneBot 启动后的 Task
        /// </summary>
        private ValueTask _startTask;
        
        public BotService(IOptions<CQHttpServerConfigModel> cqHttpServerConfigModel, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            // 配置日志
            var logger = _serviceProvider.GetService<ILogService>();
            if (logger != null) {
                Log.LogConfiguration.EnableConsoleOutput();
                //Log. = logger; 
            }

            // 配置 CQHTTP Sora
            var cqHttpConfig = cqHttpServerConfigModel?.Value;
            ServiceConfig = cqHttpConfig == null ? new ServerConfig() : cqHttpConfig.ToServiceConfig();
            SoraService = SoraServiceFactory.CreateService(ServiceConfig);
        }

        public void Start()
        {
            // 初始化指令系统
            var commandService = _serviceProvider.GetService<ICommandService>() ??
                                 throw new ArgumentNullException("", "ICommandService did not register.");
            commandService.RegisterCommand();
            commandService.RegisterEventHandler();

            // 启动 CQHTTP
            _startTask = SoraService.StartService();
        }
    }
}
