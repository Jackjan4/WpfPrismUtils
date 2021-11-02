using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace De.JanRoslan.WpfPrismUtils.Windowing {
    public interface IWindowService {



        /// <summary>
        /// Opens the WPF-Window with the given class- or XAML-name
        /// </summary>
        void OpenWindow(string name);

        void OpenWindowDialog(string name);

        void OpenWindow(string name, Dictionary<string, object> parameters);

        void OpenWindowDialog(string name, Dictionary<string, object> parameters);



        /// <summary>
        /// Closes the given window visually and removes it from the WindowsService log
        /// </summary>
        /// <param name="name">The class- or XAML-name of the Window</param>
        void CloseWindow(string name);

        Window InitWindow(string name, Type[] customTypes = null);



        /// <summary>
        /// Fully initializes and returns a WPF window. The window is added to the WindowService log with optional parameters
        /// </summary>
        /// <param name="name">XAML-name of the WPF window</param>
        /// <param name="parameters">Custom parameters that are given to the window and which it can access</param>
        /// <param name="customTypes"></param>
        /// <returns>The Window object that was initialized by this method</returns>
        Window InitWindow(string name, Dictionary<string, object> parameters, Type[] customTypes = null);


        // TODO: Maybe change the string to something like type or directly get the type with Reflection (so without parameter). If not, document the string use here properly
        Dictionary<string, object> GetWindowContext(string name);



        /// <summary>
        /// Searches and resolves a FrameWorkElement by its name and returns it. This method searches through the calling/executing assembly by default and can be added with custom assemblies.
        /// </summary>
        /// <param name="name">The class-name or XAML-name of the FrameWorkElement</param>
        /// <param name="customTypes">Custom types that should be added for being also considered for resolving. By default only the project assemblies are searched.</param>
        /// <returns></returns>
        FrameworkElement InitFrameWorkElement(string name, Type[] customTypes = null);



        /// <summary>
        /// Removes the given Window from the WindowService log. This method does not close the window! When removed from the WindowService log it can not be referenced from there again.
        /// </summary>
        /// <param name="name"></param>
        void RemoveWindow(string name);
    }
}
