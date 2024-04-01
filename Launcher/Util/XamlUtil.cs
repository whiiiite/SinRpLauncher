using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using Launcher.Classes;
using System.Windows.Controls;
using System;
using System.IO;
using System.Windows.Input;

namespace SinRpLauncher.Util
{
    public static class XamlUtil
    {

        /// <summary>
        /// Find and return iterable object of contains inside control childs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns>Iterable objects with childs of window by specified control</returns>
        public static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
                yield return (T)Enumerable.Empty<T>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);

                if (ithChild == null)
                    continue;

                if (ithChild is T ttype)
                    yield return ttype;

                foreach (T childOfChild in FindVisualChilds<T>(ithChild))
                    yield return childOfChild;
            }
        }


        /// <summary>
        /// Set cursor to all controls in window. Find first grid and set custom cursors
        /// </summary>
        /// <param name="window"></param>
        /// <exception cref="Exception"></exception>
        public static void SetCursorsToControls(Window window)
        {
            try
            {
                string cursPath = Path.Combine(InfoClass.CurrentDir, PathRoots.ImagesDirectory, PathRoots.CursDirectory) + '\\';
                Cursor cursorNormal = new Cursor(cursPath + PathRoots.NormalCurFile);
                Cursor cursorHand = new Cursor(cursPath + PathRoots.LinkCurFile);

                foreach (var grid in FindVisualChilds<Grid>(window))
                {
                    grid.Cursor = cursorNormal;
                    foreach (FrameworkElement child in grid.Children)
                    {
                        child.Cursor = cursorHand;
                    }
                }
            }
            catch (Exception)
            {
                throw; // just trust me
            }
        }


        /// <summary>
        /// Clone the wpf xaml object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T XamlCloneElement<T>(T source) where T : FrameworkElement
        {
            // read the element data
            string savedObject = System.Windows.Markup.XamlWriter.Save(source);

            // Load the XamlObject. Clone it
            StringReader stringReader = new StringReader(savedObject);
            System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader);
            T target = (T)System.Windows.Markup.XamlReader.Load(xmlReader);

            return target;
        }

    }
}
