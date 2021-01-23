using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoTest.Services;
using UnoTest.Shared.Services.Interfaces;

namespace UnoTest
{
    public partial class App
    {
        ContainerBuilder RegisterPlatformServices(ContainerBuilder builder)
        {
            builder.RegisterType<AudioPlayer>()
                .As<IMediaPlayer>()
                .InstancePerRequest() ;
            return builder;
        }
    }
}
