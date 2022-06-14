using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SerpAnalysis.Test
{
    /// <summary>
    /// this class is referenced from https://www.benday.com/2010/08/24/an-easier-way-to-unit-test-inotifypropertychanged-in-silverlightwpf/
    /// </summary>
    public class NotifyPropertyChangedTester
    {
        public NotifyPropertyChangedTester(INotifyPropertyChanged viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel", "Argument cannot be null.");
            }

            this.Changes = new List<string>();

            viewModel.PropertyChanged += new PropertyChangedEventHandler(viewModel_PropertyChanged);
        }

        void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Changes.Add(e.PropertyName);
        }

        public List<string> Changes { get; private set; }

        public void AssertChange(int changeIndex, string expectedPropertyName)
        {
            Assert.IsNotNull(Changes, "Changes collection was null.");

            Assert.IsTrue(changeIndex < Changes.Count,
                "Changes collection contains '{ 0}' items and does not have an element at index '{ 1}'.",
            Changes.Count,
            changeIndex);

            Assert.That(expectedPropertyName, Is.EqualTo(Changes[changeIndex]).NoClip);
        }
    }
}
