using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Maui.Controls;
using System;
using static Biathlon_Esthivaukan.Profil_Page;
using Biathlon_Esthivaukan.Helpers;
using System.Collections.Generic;
using System.Globalization;

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

        // Méthode pour gérer le clic sur le bouton "À propos"
        private async void OnAboutClicked(object sender, EventArgs e)
        {
            string name = "Biathlon Estivaukan";
            string creators = "Créateurs de l'application : DA SILVA Selyan, Mortagne Thomas, Labadens Charles, Ouassou Moussa";
            string yearCreated = "Année de création : 2024";
            string appVersion = "Version de l'application : 1.0";

            await DisplayAlert("À Propos de l'application", $"{name}\n\n{creators}\n{yearCreated}\n{appVersion}", "OK");


        }

        private async void OnChangeEnglishLanguageClicked(object sender, EventArgs e)
        {

            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            await Navigation.PushAsync(new MainPage(), false);

        }

        private async void OnChangeVietnameseLanguageClicked(object sender, EventArgs e)
        {

            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("vi-VN");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("vi-VN");

            await Navigation.PushAsync(new MainPage(), false);


        }

        private async void OnChangeFrenchLanguageClicked(object sender, EventArgs e)
        {

            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr_FR");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");

            await Navigation.PushAsync(new MainPage(), false);
        }

        private async void OnRunClicked(object sender, EventArgs e)
        {
            string firstDistance = FirstDistancePicker.SelectedItem?.ToString();
            string secondDistance = SecondDistancePicker.SelectedItem?.ToString();
            string thirdDistance = ThirdDistancePicker.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(PublicVariablesPP.Email) ||
                string.IsNullOrEmpty(PublicVariablesPP.Nom) ||
                string.IsNullOrEmpty(PublicVariablesPP.Prenom))
            {

                await DisplayAlert("Informations manquantes", "Veuillez entrer nom, prénom et adresse mail", "OK");
                await Navigation.PushAsync(new Profil_Page(), false);

            }
            else
            {
                if (firstDistance == null || secondDistance == null || thirdDistance == null)
                {
                    await DisplayAlert("Erreur", "Veuillez sélectionner toutes les distances.", "OK");
                    return;
                }
                else if (firstDistance == secondDistance || firstDistance == thirdDistance || secondDistance == thirdDistance)
                {
                    await DisplayAlert("Erreur", "Les trois distances doivent être différentes.", "OK");
                    return;
                }
                else
                {
                    await DisplayAlert("Distances sélectionnées", $"1ère Distance: {firstDistance}\n2ème Distance: {secondDistance}\n3ème Distance: {thirdDistance}", "OK");

                    PublicVariablesMP.fdistance = firstDistance;
                    PublicVariablesMP.sdistance = secondDistance;
                    PublicVariablesMP.tdistance = thirdDistance;
                    await Navigation.PushAsync(new Runpage(TimeSpan.Zero), false);
                }
            }



            // Logique pour traiter les distances sélectionnées


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

        public static class PublicVariablesMP
        {
            public static string fdistance { get; set; }

            public static string sdistance { get; set; }

            public static string tdistance { get; set; }
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
