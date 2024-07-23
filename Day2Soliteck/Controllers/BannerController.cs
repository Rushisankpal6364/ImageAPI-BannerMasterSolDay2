using Day2Soliteck.DAL;
using Day2Soliteck.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day2Soliteck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly User_Dal _dal;

        public BannerController(User_Dal dal)
        {
            _dal = dal;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Index()
        {
            List<User> users = new List<User>();
            try
            {
                users = _dal.getAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                //return Ok(new {StatusCode = 500, ResponseMessage = "Internal Server Error"});
            }
            return Ok(users);
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult Create(User model)
        {
            try
            {
                bool result = _dal.Insert(model);
           

            }catch (Exception ex)
            {
                return BadRequest(ex.Message); 

            }
            return Ok();
        }


        [HttpGet]
        [Route("GetById")]

        public IActionResult Edit(int id)
        {
            try
            {
                User user = _dal.GetById(id);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();    
        }



        [HttpPost]
        [Route("Update")]

        public IActionResult Edit(User model)
        {
            try
            {
                bool result = _dal.Update(model);          
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet]
        [Route("DeleteById")]

        public IActionResult Delete(int id)
        {
            try
            {
                User user = _dal.GetById(id);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpGet]
        [Route("UploadFile")]

        public Response UploadFile([FromForm] FileModel filemodel)
        {
            Response response = new Response();
            try
            {
                String path = Path.Combine(@"C:\Users\Admin\OneDrive\Desktop\MyImage", filemodel.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    filemodel.file.CopyTo(stream);
                }
                response.StatusCode = 200;
                response.ErrorMessage = "Image Created Successfully";
            }
            catch(Exception ex)
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Some Error occurred while Creating the Image...!" + ex.Message;
            }
            return response;

        }
    }
}
