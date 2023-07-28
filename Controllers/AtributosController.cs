using Microsoft.AspNetCore.Mvc;
using WebApplication2.Classes;

namespace WebApplication2.Controllers
{



    [ApiController]
    [Route("[controller]")]
    public class AtributosController : ControllerBase
    {

        public readonly Atributos att;

        public AtributosController(Atributos atri)
        {
            att = atri;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            var value= rng.Next(0, 20);
            var crit = value * rng.Next(0, 20);

            var newvalue = rng.Next(5,30);
            var dnf = newvalue - rng.Next(0, 20);


            var calculo = new[]
            {
                new {Força = rng.Next(0, 20), 
                    Inteligênci = rng.Next(0, 20), 
                    Vitalidade = rng.Next(0, 20), 
                    Sorte = rng.Next(0, 20), 
                    Crítico = crit, 
                    DanoSofrido = dnf, TXT = ""}
            };

            foreach (var item in calculo)
            {
                if (item.DanoSofrido > 20)
                {
                    var txt = new[]
                    {
                        new {Força = rng.Next(0, 20), 
                            Inteligênci = rng.Next(0, 20), 
                            Vitalidade = rng.Next(0, 20), 
                            Sorte = rng.Next(0, 20), 
                            Crítico = crit, 
                            DanoSofrido = dnf, 
                            TXT = "VOCÊ PEGOU O PASSE LIVRE PRO ONLYFANS DO BUENINHO"}
                    };

                    return Ok(txt);  
                }
            }

            return Ok(calculo);
        }

    }
}
