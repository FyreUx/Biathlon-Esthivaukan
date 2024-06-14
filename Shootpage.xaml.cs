using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Biathlon_Esthivaukan
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<CheckBoxViewModel> Items { get; set; }
        private int compteur = 0;

        public MainPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<CheckBoxViewModel>
            {
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 1" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 2" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 3" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 4" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 5" }
            };

            foreach (var item in Items)
            {
                item.PropertyChanged += CheckBoxViewModel_PropertyChanged;
            }

            BindingContext = this;
        }

        private void CheckBoxViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CheckBoxViewModel.IsChecked))
            {
                var checkBox = sender as CheckBoxViewModel;
                if (checkBox.IsChecked)
                {
                    compteur++;
                }
                else
                {
                    compteur--;
                }

                Debug.WriteLine($"Compteur: {compteur}");
            }
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            string resultatMessage = CalculateResult(compteur);
            Debug.WriteLine(resultatMessage);
        }

        private string CalculateResult(int compteur)
        {
            double resultat = (double)compteur / Items.Count * 100;
            string message = $"Votre score est de {compteur} / {Items.Count}. ({resultat}% réussi)";
            return message;
        }
    }
}
