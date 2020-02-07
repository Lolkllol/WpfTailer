using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Core.ViewModels;

namespace Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomePageViewModel>();
        }
    }
}
