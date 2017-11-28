using QuienSePega.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace QuienSePega.Controllers
{
    public class EventoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        //Metodo eliminar Evento
        public Boolean EliminarEvento(int idEvento)
        {
            Evento evento = db.Eventos.Where(e => e.Id == idEvento).FirstOrDefault();
            evento.Estado = false;
            db.Entry(evento).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public Boolean SalirEvento()
        {
            IntegrantesEvento integrante = db.IntegrantesEvento.Where(i => i.Integrante == User.Identity.Name).FirstOrDefault();
            db.Entry(integrante).State = EntityState.Deleted;
            db.SaveChanges();
            return true;
        }

        //Metodo para pegarse
        public Boolean Pegarse(int idEvento)
        {
            IntegrantesEvento integrantesNew = new IntegrantesEvento() {IdEvento =idEvento,
                                                                        Integrante = User.Identity.Name};
            db.IntegrantesEvento.Add(integrantesNew);
            db.SaveChanges();
            return true;
        }

        //Metodo que retorna un JSON con la información del evento
        //Evento/DetallesEventoById
        public JsonResult DetallesEventoById(int idEvento)
        {
            Evento evento = db.Eventos.Where(e => e.Id == idEvento).FirstOrDefault();
            Lugares origen = db.Lugares.Where(l => l.Id == evento.IdLugarOrigen).FirstOrDefault();
            Lugares destino = db.Lugares.Where(l => l.Id == evento.IdLugarDestino).FirstOrDefault();
            int numeroIntegrantes = db.IntegrantesEvento.Where(i => i.IdEvento == evento.Id).Count();
            DetallesEvento detalle = new DetallesEvento() { Creador = evento.Creador,
                                                            Origen = origen.Nombre,
                                                            Destino = destino.Nombre,
                                                            idEvento1 = evento.Id,
                                                            NumeroIntegrantes = numeroIntegrantes,
                                                            HoraInicio = evento.FechaInicio.ToString("dd-MM-yyyy"),
                                                            HoraFin = evento.FechaFin.ToString("dd-MM-yyyy"),
                                                            LatitudOrigen = origen.Latitud,
                                                            LongitudOrigen = origen.Longitud,
                                                            LatitudDestino = destino.Latitud,
                                                            LongitudDestino =destino.Longitud
            };
            return Json(detalle, JsonRequestBehavior.AllowGet);
        }

        //Metodo encargado de retornar la vista de detalle eventos
        public ActionResult DetallesEvento()
        {
            List<String> cards = new List<String>() { "fa fa-fw fa-comments","fa fa-fw fa-list",
                                                      "fa fa-fw fa-shopping-cart","fa fa-fw fa-support"};
            List<String> colors = new List<String>() { "bg-primary", "bg-warning", "bg-success", "bg-danger" };
            ViewBag.LugaresOrigen = db.Lugares.ToList().Where(l => l.Tipo == "ORIGEN");
            ViewBag.LugaresDestino = db.Lugares.ToList().Where(l => l.Tipo == "DESTINO");
            var listEventos = db.Eventos.ToList().Where(e => e.Estado == true);
            List<MostrarEvento> listMostrar = new List<MostrarEvento>();
            Random rnd = new Random();
            Random rnd2 = new Random();
            foreach (var evento in listEventos)
            {
                int r = rnd.Next(cards.Count());
                int r2 = rnd2.Next(colors.Count());
                Lugares lugarOrigen = db.Lugares.Where(l => l.Id == evento.IdLugarOrigen).FirstOrDefault();
                Lugares lugarDestino = db.Lugares.Where(l => l.Id == evento.IdLugarDestino).FirstOrDefault();
                String nombreEvento = lugarOrigen.Nombre + "-" + lugarDestino.Nombre;
                listMostrar.Add(new MostrarEvento()
                {
                    IdEvento = evento.Id,
                    NombreEvento = nombreEvento,
                    NumeroRandom = cards.ElementAt(r),
                    NumeroRandom2 = colors.ElementAt(r2)
                });
            }
            ViewBag.MostrarEvento = listMostrar;
            Evento e1 = db.Eventos.Where(e => e.Creador == User.Identity.Name && e.Estado == true).FirstOrDefault();
            if (e1 != null)
            {
                ViewBag.PuedePegarse = false;
            }
            else
            {
                IntegrantesEvento integrante = db.IntegrantesEvento.Where(i => i.Integrante == User.Identity.Name).FirstOrDefault();
                if (integrante != null)
                {
                    Evento eventoAux = db.Eventos.Where(e => e.Id == integrante.IdEvento && e.Estado == true).FirstOrDefault();
                    if(eventoAux != null) { 
                        ViewBag.PuedePegarse = false;
                    }
                    else
                    {
                        ViewBag.PuedePegarse = true;
                    }

                }
                else
                {
                    ViewBag.PuedePegarse = true;
                }
            }
            return View();
        }

        //Metodo que retorna la información del perfil 
        public ActionResult MiPerfil()
        {
            //Consultar la informacion del perfil
            //1. Validar si es creador..
            Evento evento = db.Eventos.Where(e => e.Creador == User.Identity.Name && e.Estado == true).FirstOrDefault();
            if (evento != null)
            {
                ViewBag.EsCreador = true;
                Lugares lugarOrigen = db.Lugares.Where(l => l.Id == evento.IdLugarOrigen).FirstOrDefault();
                Lugares lugarDestino = db.Lugares.Where(l => l.Id == evento.IdLugarDestino).FirstOrDefault();
                ViewBag.lugarOrigen = lugarOrigen;
                ViewBag.lugarDestino = lugarDestino;
                ViewBag.SinMapa = false;
                ViewBag.NombreOrigen = lugarOrigen.Nombre;
                ViewBag.NombreDestino = lugarDestino.Nombre;
                ViewBag.latitudOrigen = lugarOrigen.Latitud;
                ViewBag.longitudOrigen = lugarOrigen.Longitud;
                ViewBag.latitudDestino = lugarDestino.Latitud;
                ViewBag.longitudDestino = lugarDestino.Longitud;
                ViewBag.IdEvento = evento.Id;
            }
            else
            {
                ViewBag.EsCreador = false;
                //Validar si esta asociado a algun evento
                IntegrantesEvento integrante = db.IntegrantesEvento.Where(i => i.Integrante == User.Identity.Name).FirstOrDefault();
                if(integrante != null) { 
                    Evento eventoIntegrante = db.Eventos.Where(e => e.Id == integrante.IdEvento && e.Estado == true).FirstOrDefault();
                    if (eventoIntegrante != null)
                    {
                        //El usuario esta asignado a un evento
                        ViewBag.EstaEnEvento = true;
                        Lugares lugarOrigen = db.Lugares.Where(l => l.Id == eventoIntegrante.IdLugarOrigen).FirstOrDefault();
                        Lugares lugarDestino = db.Lugares.Where(l => l.Id == eventoIntegrante.IdLugarDestino).FirstOrDefault();
                        ViewBag.lugarOrigen = lugarOrigen;
                        ViewBag.lugarDestino = lugarDestino;
                        ViewBag.SinMapa = false;
                        ViewBag.NombreOrigen = lugarOrigen.Nombre;
                        ViewBag.NombreDestino = lugarDestino.Nombre;
                        ViewBag.latitudOrigen = lugarOrigen.Latitud;
                        ViewBag.longitudOrigen = lugarOrigen.Longitud;
                        ViewBag.latitudDestino = lugarDestino.Latitud;
                        ViewBag.longitudDestino = lugarDestino.Longitud;
                        ViewBag.IdEvento = eventoIntegrante.Id;

                    }
                    else
                    {
                        //El usuario no esta asignado a un evento
                        ViewBag.SinMapa = true;
                        ViewBag.EstaEnEvento = false;
                        ViewBag.EsCreador = false;
                        ViewBag.NombreOrigen = "";
                        ViewBag.NombreDestino = "";
                        ViewBag.latitudOrigen = "";
                    }
                }
                else
                {
                    ViewBag.EstaEnEvento = false;
                }
            }
            return View();
        }

        //Metodo encargado de retornar la vista inicial de Eventos
        // GET: Evento/Index
        public ActionResult Index()
        {
            List<String> cards = new List<String>() { "fa fa-fw fa-comments","fa fa-fw fa-list",
                                                      "fa fa-fw fa-shopping-cart","fa fa-fw fa-support"};
            List<String> colors = new List<String>() { "bg-primary", "bg-warning", "bg-success", "bg-danger" };
            ViewBag.LugaresOrigen = db.Lugares.ToList().Where(l => l.Tipo == "ORIGEN");
            ViewBag.LugaresDestino = db.Lugares.ToList().Where(l => l.Tipo == "DESTINO");
            var listEventos = db.Eventos.ToList().Where(e => e.Estado == true);
            List<MostrarEvento> listMostrar = new List<MostrarEvento>();
            Random rnd = new Random();
            Random rnd2 = new Random();
            foreach (var evento in listEventos)
            {
                int r = rnd.Next(cards.Count());
                int r2 = rnd2.Next(colors.Count());
                Lugares lugarOrigen = db.Lugares.Where(l => l.Id == evento.IdLugarOrigen).FirstOrDefault();
                Lugares lugarDestino = db.Lugares.Where(l => l.Id == evento.IdLugarDestino).FirstOrDefault();
                String nombreEvento = lugarOrigen.Nombre + "-" + lugarDestino.Nombre;
                listMostrar.Add(new MostrarEvento() { IdEvento = evento.Id,
                                            NombreEvento = nombreEvento ,NumeroRandom = cards.ElementAt(r),
                NumeroRandom2 = colors.ElementAt(r2)});
            }
            ViewBag.MostrarEvento = listMostrar;
            Evento e1 = db.Eventos.Where(e => e.Creador == User.Identity.Name && e.Estado == true).FirstOrDefault();
            if(e1 != null)
            {
                ViewBag.PuedePegarse = false;
            }
            else { 
                IntegrantesEvento integrante = db.IntegrantesEvento.Where(i => i.Integrante == User.Identity.Name).FirstOrDefault();
                if (integrante != null)
                {
                    Evento eventoAux = db.Eventos.Where(e => e.Id == integrante.IdEvento).FirstOrDefault();
                    if (eventoAux.Estado == true)
                    {
                        ViewBag.PuedePegarse = false;
                    }
                    else
                    {
                        ViewBag.PuedePegarse = true;
                    }
                }
                else
                {
                    ViewBag.PuedePegarse = true;
                }
            }
            return View();
        }

        //Metodo encargado de registrar el evento en BD.
        //GET Evento/InsertarEvento
        public Boolean InsertarEvento(String nombreOrigen, String nombreDestino,  String creador)
        {
            Lugares lugarOrigen = db.Lugares.Where(l => l.Nombre == nombreOrigen).FirstOrDefault();
            Lugares lugarDestino = db.Lugares.Where(l => l.Nombre == nombreDestino).FirstOrDefault();
            Evento evento = new Evento
            {
                Creador = User.Identity.Name,
                IdLugarOrigen = lugarOrigen.Id,
                IdLugarDestino = lugarDestino.Id,
                Estado = true,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now
            };
            db.Eventos.Add(evento);
            db.SaveChanges();
            return true;
        }
    }
}