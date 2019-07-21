﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Reloaded.Mod.Loader.IO.Config;

namespace Reloaded.Mod.Launcher.Utility
{
    /// <summary>
    /// Injects Reloaded into an active process.
    /// </summary>
    public class ApplicationInjector
    {
        private Process _process;
        private BasicDllInjector _injector;

        public ApplicationInjector(Process process)
        {
            _process = process;
            _injector = new BasicDllInjector(process);
        }

        /// <summary>
        /// Injects the Reloaded bootstrapper into an active process.
        /// </summary>
        /// <exception cref="ArgumentException">DLL Injection failed, likely due to bad DLL or application.</exception>
        public void Inject()
        {
            long handle = _injector.Inject(GetBootstrapperPath(_process));
            if (handle == 0)
                throw new ArgumentException(Errors.DllInjectionFailed());
        }

        private string GetBootstrapperPath(Process process)
        {
            if (process.Is64Bit())
                return IoC.Get<LoaderConfig>().Bootstrapper64Path;
            else
                return IoC.Get<LoaderConfig>().Bootstrapper32Path;
        }
    }
}