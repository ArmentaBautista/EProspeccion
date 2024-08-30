using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : Controller
    {

        private readonly ILogger<ExcelController> _logger;

        public ExcelController(ILogger<ExcelController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Exportar")]
        [Authorize(Roles = "Usuario")]
        public IActionResult Exportar()
        {
                DataTable table = new DataTable();

                table.Columns.Add("NAME");
                table.Columns.Add("ADRESS");
                table.Columns.Add("DATE");
                table.Columns.Add("INDICE");

                DataRow fila = table.NewRow();
                fila["NAME"] = "Juan";
                fila["ADRESS"] = "Stret";
                fila["DATE"] = "23/12/2023";
                fila["INDICE"] = "Ok";


                table.Rows.Add(fila);


                using var libro = new XLWorkbook();
                table.TableName = "Registros";

                var hoja = libro.Worksheets.Add(table);

                hoja.ColumnsUsed().AdjustToContents();
                //agregar tablas de tanques al excel

                using var memoria = new MemoryStream();
                libro.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;
            
        }

    }
}
