using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sandwichapp.interfaces
{
    public interface IOpenApp
    {
		void OpenExternalApp();
		Task<bool> LaunchApp(string uri);
	}
}
