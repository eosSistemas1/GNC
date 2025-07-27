using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PetroleraManagerIntranet.Web.PH.Proceso
{
    public class ProcesoPHHelper
    {
        public static string ObtenerNombreEstacion(string idEstacion)
        {
            if (idEstacion == "1") return ESTACIONES.ESTACION1;
            if (idEstacion == "2") return ESTACIONES.ESTACION2;
            if (idEstacion == "3") return ESTACIONES.ESTACION3;
            if (idEstacion == "4") return ESTACIONES.ESTACION4;
            return string.Empty;
        }

        public static string ObtenerTituloProcesoPH(PasosProcesoPH value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Calculo que permite saber si la PH esta aprobada o rechazada
        /// </summary>
        /// <returns>
        /// Devuelve: True si la ph es aprobada
        ///           False si es rechazada
        /// </returns>
        public bool EvaluacionPH(double nivelAguaDespresurizadoCilindro,
                                  double nivelAguaPesionPrueba,
                                  double presionPrueba,
                                  double temperaturaAmbiente,
                                  int idValvula,
                                  double pesoCilindroLlenoAgua,
                                  double pesoCilindroVacio)
        {
            double coeficienteBarToPSI = double.Parse("0.068947569");
            double coeficientePesoAguaPresionPrueba = double.Parse("0.45359");

            double presionPrueba_pp = presionPrueba / coeficienteBarToPSI;

            PendienteConstante pendienteConstante1 = new PendienteConstante().GetPendienteConstanteInferior(temperaturaAmbiente);
            PendienteConstante pendienteConstante2 = new PendienteConstante().GetPendienteConstanteSuperior(temperaturaAmbiente);
            double f1 = pendienteConstante1.Constante + (pendienteConstante1.Pendiente * presionPrueba_pp);
            double f2 = pendienteConstante2.Constante + (pendienteConstante2.Pendiente * presionPrueba_pp);
            double factorCompresibilidad_f = (f1 + f2) / 2;

            double pesoEspecificoAgua = new PesoEspecificoAgua().GetPesoEspecificoAguaPorTemperatura(temperaturaAmbiente);

            double pesoValvulaCarga = new ValvulaCarga().GetValvulaCargaByID(idValvula).Peso;

            double pesoAguaContenida = pesoCilindroLlenoAgua - pesoCilindroVacio - pesoValvulaCarga;
            double pesoPresionAguaPrueba_w = (pesoAguaContenida + (nivelAguaPesionPrueba * pesoEspecificoAgua / 1000)) / coeficientePesoAguaPresionPrueba;

            double correccionCompresibilidad_cc = factorCompresibilidad_f * pesoPresionAguaPrueba_w * presionPrueba_pp;

            double expansionPermanente = nivelAguaDespresurizadoCilindro;
            double expansionTotal = nivelAguaPesionPrueba - correccionCompresibilidad_cc;
            //double expansionElastica = expansionTotal - expansionPermanente;

            double porcentajeExpansionPermanente = 100 * (expansionPermanente / expansionTotal);

            return porcentajeExpansionPermanente < 10;
        }
    }

    public class PesoEspecificoAgua
    {
        public double GetPesoEspecificoAguaPorTemperatura(double temperaturaAmbiente)
        {
            return Tablas.PesoEspecificoAguaPorTemperatura().Where(p => p.Temperatura == temperaturaAmbiente).FirstOrDefault().PesoEspecifico;
        }

        public double Temperatura { get; set; }
        public double PesoEspecifico { get; set; }
    }

    public class PendienteConstante
    {
        public PendienteConstante GetPendienteConstanteInferior(double temperaturaAmbiente)
        {
            double tf = (temperaturaAmbiente * 9 / 5) + 32;

            double valor = double.Parse(tf.ToString("0"));

            if (valor % 2 == 0)
            {
                tf = valor - 2;
            }
            else
            {
                tf = valor - 1;
            }

            var constantes = Tablas.PendienteConstante();
            return constantes.Where(p => p.Temperatura == tf).FirstOrDefault();
        }

        public PendienteConstante GetPendienteConstanteSuperior(double temperaturaAmbiente)
        {
            double tf = (temperaturaAmbiente * 9 / 5) + 32;

            double valor = double.Parse(tf.ToString("0"));

            if (valor % 2 == 0)
            {
                tf = valor;
            }
            else
            {
                tf = valor + 1;
            }

            var constantes = Tablas.PendienteConstante();
            return constantes.Where(p => p.Temperatura == tf).FirstOrDefault();
        }

        public double Temperatura { get; set; }
        public double Pendiente { get; set; }
        public double Constante { get; set; }
    }

    public class ValvulaCarga
    {
        public ValvulaCarga GetValvulaCargaByID(int idValvulaCarga)
        {
            return Tablas.ValvulaCarga().Where(v => v.ID == idValvulaCarga).FirstOrDefault();
        }

        public int ID { get; set; }
        public string Descripcion { get; set; }
        public double Peso { get; set; }
    }

    public static class Tablas
    {
        public static List<PesoEspecificoAgua> PesoEspecificoAguaPorTemperatura()
        {
            List<PesoEspecificoAgua> valores = new List<PesoEspecificoAgua>();

            valores.Add(new PesoEspecificoAgua() { Temperatura = 0.0f, PesoEspecifico = 0.99987f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 1.0f, PesoEspecifico = 0.99993f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 2.0f, PesoEspecifico = 0.99997f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 3.0f, PesoEspecifico = 0.99999f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 4.0f, PesoEspecifico = 1.00000f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 5.0f, PesoEspecifico = 0.99999f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 6.0f, PesoEspecifico = 0.99997f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 7.0f, PesoEspecifico = 0.99993f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 8.0f, PesoEspecifico = 0.99988f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 9.0f, PesoEspecifico = 0.99981f });

            valores.Add(new PesoEspecificoAgua() { Temperatura = 10.0f, PesoEspecifico = 0.99973f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 11.0f, PesoEspecifico = 0.99963f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 12.0f, PesoEspecifico = 0.99952f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 13.0f, PesoEspecifico = 0.99940f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 14.0f, PesoEspecifico = 0.99927f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 15.0f, PesoEspecifico = 0.99913f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 16.0f, PesoEspecifico = 0.99897f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 17.0f, PesoEspecifico = 0.99880f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 18.0f, PesoEspecifico = 0.99862f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 19.0f, PesoEspecifico = 0.99843f });

            valores.Add(new PesoEspecificoAgua() { Temperatura = 20.0f, PesoEspecifico = 0.99823f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 21.0f, PesoEspecifico = 0.99802f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 22.0f, PesoEspecifico = 0.99780f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 23.0f, PesoEspecifico = 0.99756f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 24.0f, PesoEspecifico = 0.99732f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 25.0f, PesoEspecifico = 0.99707f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 26.0f, PesoEspecifico = 0.99681f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 27.0f, PesoEspecifico = 0.99654f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 28.0f, PesoEspecifico = 0.99626f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 29.0f, PesoEspecifico = 0.99597f });

            valores.Add(new PesoEspecificoAgua() { Temperatura = 30.0f, PesoEspecifico = 0.99567f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 31.0f, PesoEspecifico = 0.99537f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 32.0f, PesoEspecifico = 0.99505f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 33.0f, PesoEspecifico = 0.99473f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 34.0f, PesoEspecifico = 0.99440f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 35.0f, PesoEspecifico = 0.99406f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 36.0f, PesoEspecifico = 0.99371f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 37.0f, PesoEspecifico = 0.99335f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 38.0f, PesoEspecifico = 0.99299f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 39.0f, PesoEspecifico = 0.99262f });

            valores.Add(new PesoEspecificoAgua() { Temperatura = 40.0f, PesoEspecifico = 0.99224f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 41.0f, PesoEspecifico = 0.99185f });
            valores.Add(new PesoEspecificoAgua() { Temperatura = 42.0f, PesoEspecifico = 0.99146f });

            return valores;
        }

        public static List<PendienteConstante> PendienteConstante()
        {
            List<PendienteConstante> valores = new List<PendienteConstante>();
            valores.Add(new PendienteConstante() { Temperatura = 38, Pendiente = -0.0000000155f, Constante = 0.001560f });
            valores.Add(new PendienteConstante() { Temperatura = 40, Pendiente = -0.0000000154f, Constante = 0.001551f });
            valores.Add(new PendienteConstante() { Temperatura = 42, Pendiente = -0.0000000153f, Constante = 0.001541f });
            valores.Add(new PendienteConstante() { Temperatura = 44, Pendiente = -0.0000000152f, Constante = 0.001531f });
            valores.Add(new PendienteConstante() { Temperatura = 46, Pendiente = -0.0000000151f, Constante = 0.001522f });
            valores.Add(new PendienteConstante() { Temperatura = 48, Pendiente = -0.0000000151f, Constante = 0.001514f });
            valores.Add(new PendienteConstante() { Temperatura = 50, Pendiente = -0.0000000150f, Constante = 0.001506f });
            valores.Add(new PendienteConstante() { Temperatura = 52, Pendiente = -0.0000000149f, Constante = 0.001499f });
            valores.Add(new PendienteConstante() { Temperatura = 54, Pendiente = -0.0000000148f, Constante = 0.001493f });
            valores.Add(new PendienteConstante() { Temperatura = 56, Pendiente = -0.0000000147f, Constante = 0.001486f });
            valores.Add(new PendienteConstante() { Temperatura = 58, Pendiente = -0.0000000147f, Constante = 0.001480f });
            valores.Add(new PendienteConstante() { Temperatura = 60, Pendiente = -0.0000000146f, Constante = 0.001475f });
            valores.Add(new PendienteConstante() { Temperatura = 62, Pendiente = -0.0000000146f, Constante = 0.001469f });
            valores.Add(new PendienteConstante() { Temperatura = 64, Pendiente = -0.0000000145f, Constante = 0.001464f });
            valores.Add(new PendienteConstante() { Temperatura = 66, Pendiente = -0.0000000144f, Constante = 0.001460f });
            valores.Add(new PendienteConstante() { Temperatura = 68, Pendiente = -0.0000000144f, Constante = 0.001456f });
            valores.Add(new PendienteConstante() { Temperatura = 70, Pendiente = -0.0000000144f, Constante = 0.001452f });
            valores.Add(new PendienteConstante() { Temperatura = 72, Pendiente = -0.0000000143f, Constante = 0.001448f });
            valores.Add(new PendienteConstante() { Temperatura = 74, Pendiente = -0.0000000143f, Constante = 0.001445f });
            valores.Add(new PendienteConstante() { Temperatura = 76, Pendiente = -0.0000000142f, Constante = 0.001442f });
            valores.Add(new PendienteConstante() { Temperatura = 78, Pendiente = -0.0000000142f, Constante = 0.001438f });
            valores.Add(new PendienteConstante() { Temperatura = 80, Pendiente = -0.0000000142f, Constante = 0.001435f });
            valores.Add(new PendienteConstante() { Temperatura = 82, Pendiente = -0.0000000141f, Constante = 0.001432f });
            valores.Add(new PendienteConstante() { Temperatura = 84, Pendiente = -0.0000000141f, Constante = 0.001429f });
            valores.Add(new PendienteConstante() { Temperatura = 86, Pendiente = -0.0000000141f, Constante = 0.001427f });
            valores.Add(new PendienteConstante() { Temperatura = 88, Pendiente = -0.0000000141f, Constante = 0.001425f });
            valores.Add(new PendienteConstante() { Temperatura = 90, Pendiente = -0.0000000141f, Constante = 0.001423f });
            valores.Add(new PendienteConstante() { Temperatura = 92, Pendiente = -0.0000000141f, Constante = 0.001421f });
            valores.Add(new PendienteConstante() { Temperatura = 94, Pendiente = -0.0000000140f, Constante = 0.001420f });
            valores.Add(new PendienteConstante() { Temperatura = 96, Pendiente = -0.0000000140f, Constante = 0.001418f });
            valores.Add(new PendienteConstante() { Temperatura = 98, Pendiente = -0.0000000140f, Constante = 0.001417f });
            valores.Add(new PendienteConstante() { Temperatura = 100, Pendiente = -0.0000000140f, Constante = 0.001415f });

            return valores;
        }

        public static List<ValvulaCarga> ValvulaCarga()
        {
            List<ValvulaCarga> valores = new List<ValvulaCarga>();
            valores.Add(new ValvulaCarga() { ID = 1, Descripcion = "Valvula 1", Peso = 200f });
            return valores;
        }
    }
}