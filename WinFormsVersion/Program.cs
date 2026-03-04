using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LimpiezaWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private readonly Button btnCalcular;
        private readonly ListBox listBox;
        private readonly Label lblResumen;

        public MainForm()
        {
            Text = "Estimación Limpieza";
            Width = 500;
            Height = 400;

            btnCalcular = new Button { Text = "Calcular", Top = 10, Left = 10, Width = 100 };
            btnCalcular.Click += async (s, e) => await CalcularAsync();

            listBox = new ListBox { Top = 50, Left = 10, Width = 460, Height = 250 };
            lblResumen = new Label { Top = 310, Left = 10, Width = 460, Height = 40 };

            Controls.Add(btnCalcular);
            Controls.Add(listBox);
            Controls.Add(lblResumen);

            Load += (s, e) => CargarZonas();
        }

        private void CargarZonas()
        {
            listBox.Items.Clear();
            var zonas = new Dictionary<string, (int largo, int ancho)>
            {
                { "Zona 1", (500, 150) },
                { "Zona 2", (480, 101) },
                { "Zona 3", (309, 480) },
                { "Zona 4", (90, 220) }
            };
            foreach (var z in zonas)
                listBox.Items.Add($"{z.Key}: {z.Value.largo} x {z.Value.ancho}");
        }

        private async Task CalcularAsync()
        {
            btnCalcular.Enabled = false;
            var zonas = new Dictionary<string, (int largo, int ancho)>
            {
                { "Zona 1", (500, 150) },
                { "Zona 2", (480, 101) },
                { "Zona 3", (309, 480) },
                { "Zona 4", (90, 220) }
            };
            double tasaLimpieza = 1000.0;

            var areas = new Dictionary<string, int>();
            await Task.Run(() =>
            {
                foreach (var z in zonas)
                    areas[z.Key] = z.Value.largo * z.Value.ancho;
            });

            listBox.Items.Clear();
            foreach (var a in areas)
                listBox.Items.Add($"{a.Key}: {a.Value} cm²");

            int superficieTotal = areas.Values.Sum();
            double tiempoLimpieza = superficieTotal / tasaLimpieza;
            lblResumen.Text = $"Total: {superficieTotal} cm² — {tiempoLimpieza:F2}s ({tiempoLimpieza/60:F2} min)";

            btnCalcular.Enabled = true;
        }
    }
}