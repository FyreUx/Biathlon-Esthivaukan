﻿using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Biathlon_Esthivaukan
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            FirstDistancePicker.SelectedIndexChanged += DistancePickerSelectedIndexChanged;
            SecondDistancePicker.SelectedIndexChanged += DistancePickerSelectedIndexChanged;
            ThirdDistancePicker.SelectedIndexChanged += DistancePickerSelectedIndexChanged;
        }

        private async void OnRunClicked(object sender, EventArgs e)
        {
            string firstDistance = FirstDistancePicker.SelectedItem?.ToString();
            string secondDistance = SecondDistancePicker.SelectedItem?.ToString();
            string thirdDistance = ThirdDistancePicker.SelectedItem?.ToString();

            if (firstDistance == null || secondDistance == null || thirdDistance == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner toutes les distances.", "OK");
                return;
            }

            // Vérifier à nouveau l'unicité des distances sélectionnées
            if (firstDistance == secondDistance || firstDistance == thirdDistance || secondDistance == thirdDistance)
            {
                await DisplayAlert("Erreur", "Les trois distances doivent être différentes.", "OK");
                return;
            }

            // Logique pour traiter les distances sélectionnées
            await DisplayAlert("Distances sélectionnées",
                $"1ère Distance: {firstDistance}\n2ème Distance: {secondDistance}\n3ème Distance: {thirdDistance}", "OK");

            // Naviguer vers la prochaine page avec les distances sélectionnées
            // await Navigation.PushAsync(new NextPage(firstDistance, secondDistance, thirdDistance));
        }

        private async void OnTrackingClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SuiviPerf(), false);
        }

        private async void OnProfilClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profil_Page(), false);
        }

        private void DistancePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;

            // Récupérer les valeurs sélectionnées actuelles
            string firstDistance = FirstDistancePicker.SelectedItem?.ToString();
            string secondDistance = SecondDistancePicker.SelectedItem?.ToString();
            string thirdDistance = ThirdDistancePicker.SelectedItem?.ToString();

            // Vérifier l'unicité des valeurs sélectionnées
            if (firstDistance != null && secondDistance != null && thirdDistance != null)
            {
                if (firstDistance == secondDistance || firstDistance == thirdDistance || secondDistance == thirdDistance)
                {
                    // Réinitialiser le Picker où une sélection invalide a été faite
                    if (picker == FirstDistancePicker)
                        FirstDistancePicker.SelectedIndex = -1;
                    else if (picker == SecondDistancePicker)
                        SecondDistancePicker.SelectedIndex = -1;
                    else if (picker == ThirdDistancePicker)
                        ThirdDistancePicker.SelectedIndex = -1;

                    // Afficher un message d'erreur
                    DisplayAlert("Erreur", "Les trois distances doivent être différentes.", "OK");
                }
            }
        }
    }
}
