using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using De.JanRoslan.WpfPrismUtils.Windowing.Exceptions;
using Unity;

namespace De.JanRoslan.WpfPrismUtils.Windowing {
    public class WindowService : IWindowService {



        /// <summary>
        /// The unity container of this application
        /// </summary>
        private readonly IUnityContainer _container;



        /// <summary>
        /// Keeping track of which windows are currently open, so we can list them or close them later
        /// </summary>
        private readonly Dictionary<string, Tuple<Window, Dictionary<string, object>>> _windows;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public WindowService(IUnityContainer container) {
            _container = container;
            _windows = new Dictionary<string, Tuple<Window, Dictionary<string, object>>>();
        }



        /// <summary>
        /// Opens the WPF-Window with the given class- or XAML-name
        /// </summary>
        public void OpenWindow(string name) {
            OpenWindow(name, null);
        }

        public void OpenWindowDialog(string name) {
            OpenWindowDialog(name, null);
        }

        public void OpenWindow(string name, Dictionary<string, object> parameters) {
            Type[] types = Assembly.GetCallingAssembly().GetTypes();
            InitWindow(name, parameters, types).Show();
        }



        public void OpenWindowDialog(string name, Dictionary<string, object> parameters) {
            Type[] types = Assembly.GetCallingAssembly().GetTypes();
            InitWindow(name, parameters, types).ShowDialog();
        }



        /// <summary>
        /// Closes the given window visually and removes it from the WindowsService log
        /// </summary>
        /// <param name="name">The class- or XAML-name of the Window</param>
        public void CloseWindow(string name) {
            _windows[name].Item1.Close();
            RemoveWindow(name);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="customTypes"></param>
        /// <returns></returns>
        public Window InitWindow(string name, Type[] customTypes = null) {
            return InitWindow(name, null, customTypes);
        }



        /// <summary>
        /// Fully initializes and returns a WPF window. The window is added to the WindowService log with optional parameters
        /// </summary>
        /// <param name="name">XAML-name of the WPF window</param>
        /// <param name="parameters">Custom parameters that are given to the window and which it can access</param>
        /// <param name="customTypes"></param>
        /// <returns>The Window object that was initialized by this method</returns>
        public Window InitWindow(string name, Dictionary<string, object> parameters, Type[] customTypes = null) {
            Type[] types = customTypes ?? Assembly.GetCallingAssembly().GetTypes();

            // Init FrameWorkElement - This can throw exceptions (NotAFrameworkElementException, FrameworkElementNotFoundException), a function above this should catch that
            FrameworkElement obj = InitFrameWorkElement(name, types);

            if(obj is not Window) {
                throw new NotAWindowException();
            }

            Window win = (Window) obj;
            _windows[name] = new Tuple<Window, Dictionary<string, object>>(win, parameters ?? new Dictionary<string, object>());
            return win;
        }



        /// <summary>
        /// Returns the parameter-dictionary for a given window
        /// </summary>
        /// <param name="name">The class- or XAML-name of the WPF window</param>
        /// <returns>Parameter-dictionary that was saved for the given window</returns>
        public Dictionary<string, object> GetWindowContext(string name) {
            return _windows[name].Item2;
        }







        /// <summary>
        /// Searches and resolves a FrameWorkElement by its name and returns it. This method searches through the calling/executing assembly by default and can be added with custom assemblies.
        /// </summary>
        /// <param name="name">The class-name or XAML-name of the FrameWorkElement</param>
        /// <param name="customTypes">Custom types that should be added for being also considered for resolving. By default only the project assemblies are searched.</param>
        /// <returns></returns>
        public FrameworkElement InitFrameWorkElement(string name, Type[] customTypes = null) {
            var list = new HashSet<Type[]>
            {
                Assembly.GetCallingAssembly().GetTypes(),
                Assembly.GetExecutingAssembly().GetTypes(),
                Assembly.GetEntryAssembly()?.GetTypes()
            };

            // Check if custom assembly was delivered
            if(customTypes != null) {
                list.Add(customTypes);
            }

            object obj = null;
            foreach(Type[] types in list) {

                Type first = types?.FirstOrDefault(t => t.Name == name);
                if(first == null)
                    continue;

                obj = _container.Resolve(first);
                if(obj != null)
                    break;
            }
            if(obj == null) throw new FrameworkElementNotFoundException();

            if(obj is not FrameworkElement) throw new NotAFrameworkElementException();

            return (FrameworkElement) obj;
        }



        /// <summary>
        /// Removes the given Window from the WindowService log. This method does not close the window! When removed from the WindowService log it can not be referenced from there again.
        /// </summary>
        /// <param name="name"></param>
        public void RemoveWindow(string name) {
            _windows.Remove(name);
        }
    }
}