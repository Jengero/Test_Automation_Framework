﻿using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace TAF.Core.Elements
{
    public class ElementsList<T>
    {
        private readonly List<T> _elements = new List<T>();
        private readonly By _locator;
        private readonly IWebElement _element;

        public ElementsList(By locator) 
        {

            this._locator = locator;
        }

        public ElementsList(IWebElement element) 
        {
            this._element = element;
        }

        public List<T> GetElements()
        {
            UpdateElements();
            return _elements.ToList();
        }

        public List<TResult> Select<TResult>(Func<T, TResult> func)
        {
            UpdateElements();
            return _elements.Select(func).ToList();
        }

        private void UpdateElements ()
        {
            if (_elements.ToList().Count != 0) return;
            var foundElements = FindElements(_locator);
            foreach (var foundElement in foundElements)
            {
                var elementInstance = (T)Activator.CreateInstance(typeof(T), foundElement);
                _elements.Add(elementInstance);
            }
        }

        public IWebElement FindElement(By by)
        {
            if (_element != null)
            {
                return _element.FindElement(by);
            }
            else
            {
                return Browser.Browser.NewBrowser.FindElement(by);
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            if (_element != null)
            {
                return _element.FindElements(by);
            }
            else
            {
                return Browser.Browser.NewBrowser.FindElements(by);
            }
        }
    }
}