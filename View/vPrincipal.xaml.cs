using System;
using Microsoft.Maui.Controls;

namespace JPillajoS2Actividad.View
{
    public partial class vPrincipal : ContentPage
    {
        public vPrincipal()
        {
            InitializeComponent();
        }

        private void OnNotaChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                if (!double.TryParse(entry.Text, out double value) || value < 0.1 || value > 10)
                {
                    // Puedes optar por borrar el texto o mostrar un mensaje temporal aquí.
                    entry.Text = string.Empty;
                }
            }
        }

        private void btnCalcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener datos de las entradas
                double notaSeguimiento1 = double.Parse(txtseg1.Text);
                double examen1 = double.Parse(txtEx1.Text);
                double notaSeguimiento2 = double.Parse(txtseg2.Text);
                double examen2 = double.Parse(txtEx2.Text);

                // Verificar que todas las notas estén en el rango de 0.1 a 10 y que se haya seleccionado un estudiante
                if (notaSeguimiento1 >= 0.1 && notaSeguimiento1 <= 10 &&
                    examen1 >= 0.1 && examen1 <= 10 &&
                    notaSeguimiento2 >= 0.1 && notaSeguimiento2 <= 10 &&
                    examen2 >= 0.1 && examen2 <= 10 &&
                    pkEstudiantes.SelectedItem != null)
                {
                    // Calcular notas parciales y final
                    double notaParcial1 = (notaSeguimiento1 * 0.3) + (examen1 * 0.2);
                    double notaParcial2 = (notaSeguimiento2 * 0.3) + (examen2 * 0.2);
                    double notaFinal = notaParcial1 + notaParcial2;

                    // Determinar el estado del estudiante
                    string estado;
                    if (notaFinal >= 7)
                    {
                        estado = "Aprobado";
                    }
                    else if (notaFinal >= 5 && notaFinal < 7)
                    {
                        estado = "Complementario";
                    }
                    else
                    {
                        estado = "Reprobado";
                    }

                    // Obtener la fecha seleccionada
                    string fecha = dpFecha.Date.ToString("MM/dd/yyyy");

                    // Mostrar resultados en un DisplayAlert
                    DisplayAlert("NOTA FINAL",
                        $"Nombre: {pkEstudiantes.SelectedItem}\n" +
                        $"Fecha: {fecha}\n" +
                        $"Nota Parcial 1: {notaParcial1:F2}\n" +
                        $"Nota Parcial 2: {notaParcial2:F2}\n" +
                        $"Nota Final: {notaFinal:F2}\n" +
                        $"Estado: {estado}",
                        "OK");
                }
                else
                {
                    // Mostrar mensaje de error si alguna nota no está en el rango válido o no se ha seleccionado un estudiante
                    DisplayAlert("Error", "Por favor, ingrese todas las notas en el rango de 0.1 a 10 y seleccione un estudiante.", "OK");
                }
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Por favor, ingrese todas las notas correctamente.", "OK");
            }
        }
    }
}
