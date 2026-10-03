using Aplicacion.Servicios;
using Persistencia.Entidades;
using Xunit;
using Xunit.Sdk;

namespace Tests
{
   public class SimulacionServicioTest
   {
      [Fact]
      public void AgregarDispositivo_DatosValidos()
      {
         var simulacion = new Simulacion(1);
         var router = new Router(
            1,
            "Router Principal",
            "192.168.1.1",
            "aa:bb:cc:dd:ee:01",
            true,
            0,
            0,
            10
         );
         var servicio = new SimulacionServicio();
         servicio.AgregarDispositivo(simulacion,router);
         Assert.Single(simulacion.Dispositivos);
         Assert.Same(router, simulacion.Dispositivos[0]);
      }
      [Fact]
      public void AgregarPaquete_DatosValidos()
      {
         var simulacion = new Simulacion(1);
         var paquete = new PaqueteRed(
            10,
            1,
            "192.168.1.10",
            "192.168.1.20",
            "AA:BB:CC:DD:EE:01",
            "AA:BB:CC:DD:EE:02",
            500,
            "TCP",
            "Datos en Prueba",
            DateTime.Now
         );
         var servicio= new SimulacionServicio();
         servicio.AgregarPaquete(simulacion,paquete);
         Assert.Single(simulacion.Paquetes);
         Assert.Same(paquete, simulacion.Paquetes[0]);
      }
      [Fact]
      public void ProcesarPaquete_RouterEncendido()
      {
         var simulacion = new Simulacion(1);
         var router = new Router(
            1,
            "Router Principal",
            "192.168.1.1",
            "AA:BB:CC:DD:EE:01",
            true,
            0,
            0,
            10
         );
         var paquete = new PaqueteRed(
            10,
            1,
            "192.168.1.10",
            "192.168.1.20",
            "AA:BB:CC:DD:EE:02",
            "AA:BB:CC:DD:EE:03",
            500,
            "TCP",
            "Datos en prueba",
            DateTime.Now
         );
         var servicio = new SimulacionServicio();
         servicio.AgregarDispositivo(simulacion, router);
         servicio.ProcesarPaquete(simulacion, paquete);
         Assert.True(paquete.Procesado);
         Assert.Equal(10, paquete.LatenciaAcumulada);
         Assert.Equal(9, paquete.Ttl);
      }
      [Fact]
      public void ProcesarPaquete_RouterApagado_DescartaPaquete()
      {
         Simulacion simulacion = new Simulacion (1);
         Router router = new Router (
            1,
            "Router Principal",
            "192.168.1.1",
            "AA:BB:CC:DD:EE:01",
            false,
            0,
            0,
            10
         );
         PaqueteRed paquete = new PaqueteRed(
            10,
            1,
            "192.168.1.10",
            "192.168.1.20",
            "AA:BB:CC:DD:EE:01",
            "AA:BB:CC:DD:EE:02",
             500,
            "TCP",
            "Datos en Prueba",
        DateTime.Now
         );
         SimulacionServicio servicio = new SimulacionServicio();
         servicio.AgregarDispositivo(simulacion,router);
         servicio.AgregarPaquete(simulacion, paquete);
         servicio.ProcesarPaquete(simulacion, paquete);
         Assert.False(paquete.Procesado);
         Assert.Equal(10, paquete.LatenciaAcumulada);
         Assert.Equal(9, paquete.Ttl);
      }
      [Fact]
      public void ProcesarPaquete_DosDispositivos_AcumulaLatenciaTtl()
      {
         Simulacion simulacion = new Simulacion(1);
         Router router = new Router(
            1,
           "Router Principal",
           "192.168.1.1",
           "AA:BB:CC:DD:EE:01",
            true,
            0,
            0,
            10
         );
         Switch switchRed = new Switch(
            2,
            "Switch Principal",
            "192.168.1.2",
            "AA:BB:CC:DD:EE:02",
            true,
            25,
            5,
            false,
            0,
            5
         );
         PaqueteRed paquete = new PaqueteRed(
            10,
            1,
            "192.168.1.10",
            "192.168.1.20",
            "AA:BB:CC:DD:EE:01",
            "AA:BB:CC:DD:EE:02",
            500,
            "TCP",
            "Datos en Prueba",
            DateTime.Now            
         );
         SimulacionServicio servicio = new SimulacionServicio();
         servicio.AgregarDispositivo(simulacion, router);
         servicio.AgregarDispositivo(simulacion, switchRed);
         servicio.AgregarPaquete(simulacion, paquete);
         
         servicio.ProcesarPaquete(simulacion, paquete);

         Assert.True(paquete.Procesado);
         Assert.Equal(15, paquete.LatenciaAcumulada);
         Assert.Equal(8,paquete.Ttl);
      }
      [Fact]
      public void ProcesarPaquete_Firewall_DescartarPaquete()
      {
         Simulacion simulacion = new Simulacion(1);
         Firewall firewall = new Firewall(
            1,
            "Firewall Principal",
            "192.168.1.3",
            "AA:BB:CC:DD:EE:03",
            true,
            true,
            "bloquear",
            2,
            true,
            false,
            "Red",
            8
         );
         PaqueteRed paquete = new PaqueteRed(
           10,
           1,
           "192.168.1.10",
           "192.168.1.20",
           "AA:BB:CC:DD:EE:01",
           "AA:BB:CC:DD:EE:02",
           500,
           "TCP",
           "Datos en Prueba",
           DateTime.Now
         );
         SimulacionServicio servicio = new SimulacionServicio();

         servicio.AgregarDispositivo(simulacion, firewall);
         servicio.AgregarPaquete(simulacion, paquete);

         servicio.ProcesarPaquete(simulacion,paquete);

         Assert.False(paquete.Procesado);
         Assert.Equal(8, paquete.LatenciaAcumulada);
         Assert.Equal(9, paquete.Ttl);
      }
   }
}