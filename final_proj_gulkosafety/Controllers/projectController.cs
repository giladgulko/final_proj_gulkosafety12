﻿using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class projectController : ApiController
    {
        public List<project> Get()
        {
            try
            {
                project p = new project();
                List<project> pList = p.Read();
                return pList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       
        public List<project> Get(string userEmail)
        {
            try
            {
                project p = new project();
                List<project> pList = p.Read(userEmail);
                return pList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public HttpResponseMessage Post([FromBody] project p)
        {
            try
            {
                {
                    p.Insert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, p);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public HttpResponseMessage PutProjectDetails([FromBody] project p)
        {
            try
            {
                {
                    p.UpdateProjectDetails();

                }

                return Request.CreateResponse(HttpStatusCode.Created, p);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


        //public HttpResponseMessage PutProjectSafetyLevel(int project_num, double safety_lvl)
        //{
        //    project p = new project();
        //    try
        //    {
        //        {
        //            p.UpdateProjectSafetyLevel(project_num, safety_lvl);

        //        }

        //        return Request.CreateResponse(HttpStatusCode.Created, p);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
        //    }
        //}

        public HttpResponseMessage PutProjectUsers(int project_num, [FromBody] project p)
        {
            try
            {
                {
                    p.UpdateProjectUser();
                }

                return Request.CreateResponse(HttpStatusCode.Created, p);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public HttpResponseMessage DeleteProject([FromBody] project p)
        {

            try
            {
                {
                    p.DeleteProject(p.Project_num);
                }

                return Request.CreateResponse(HttpStatusCode.Created, p);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

    }
}