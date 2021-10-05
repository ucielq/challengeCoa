using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCOA.Data;
using WebApiCOA.Models;

namespace WebApiCOA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly WebApiCOAContext _context;

        public UsuariosController(WebApiCOAContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<Respuesta> GetUsuario()
        {

            var users = await _context.Usuario.ToListAsync();
            Respuesta oRespuesta = new Respuesta
            {
                Data = users,
                Status = Ok()
            };
            return oRespuesta;

        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<Respuesta> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                Respuesta oRespuesta = new Respuesta
                {
                    Status = NotFound(),
                    Data = null
                };
                return oRespuesta;
            }

            return new Respuesta
            {
                Data = usuario,
                Status = Ok()
            };
        }

        // PUT: api/Usuarios/{id}
        [HttpPut]
        public async Task<Respuesta> PutUsuario(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Respuesta
                {
                    Data = usuario,
                    Status = Ok(),
                };
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return new Respuesta
                    {
                        Status = NotFound(),
                        Error = e.InnerException.Message
                    };
                }
                else
                {
                    return new Respuesta
                    {
                        Status = new StatusCodeResult(400),
                        Error = e.InnerException.Message
                    };
                }
            }
        }

        // POST: api/Usuarios
        [HttpPost("{id}")]
        public async Task<Respuesta> PostUsuario(int id, Usuario usuario)
        {

            if (id != usuario.Id)
            {

                return new Respuesta
                {
                    Data = usuario,
                    Status = BadRequest(),
                    Error = "No coincide el id pasado por parámetros con el id del objeto Usuario"
                };
            }

            var user = await _context.Usuario.FindAsync(id);

            if (user != null)
            {
                return new Respuesta
                {
                    Data = usuario,
                    Status = BadRequest(),
                    Error = $"Ya existe el usuario con id {id}"
                }; ;
            }
            _context.Usuario.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
                return new Respuesta
                {
                    Data = usuario,
                    Status = new StatusCodeResult(200),
                };

            }
            catch (Exception e)
            {
                return new Respuesta
                {
                    Error = e.InnerException.Message,
                    Status = BadRequest()
                };
            }
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<Respuesta> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return new Respuesta
                {
                    Data = usuario,
                    Status = NotFound(),
                    Error = $"No existe el usuario con id {id}"
                };
            }

            _context.Usuario.Remove(usuario);
            try
            {
                await _context.SaveChangesAsync();
                return new Respuesta
                {
                    Data = usuario,
                    Status = Ok()
                };
            }
            catch (Exception e)
            {
                return new Respuesta
                {
                    Data = usuario,
                    Status = BadRequest(),
                    Error = e.InnerException.ToString()
                };
            }

        }

        private bool UsuarioExists(int id)
        {
            try
            {
                return _context.Usuario.Any(e => e.Id == id);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }
    }
}
