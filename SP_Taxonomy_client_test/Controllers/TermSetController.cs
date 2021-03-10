using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using SP_Taxonomy_client_test.Infrastructure;
using SP_Taxonomy_client_test.Models;

namespace SP_Taxonomy_client_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermSetController : ControllerBase
    {
        private readonly ITermSet _spTermsService;

        private readonly IConfiguration _config;

        public TermSetController(ITermSet _spTermsService, IConfiguration _config)
        {
            this._spTermsService = _spTermsService;
            this._config = _config;
        }

        // GET api/termset
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<TermModel>>> GetTerms([FromQuery(Name = "termset")] string _termset)
        {
            return await this._spTermsService.GetAllTerms(_termset);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        /// <summary>
        /// Create new term or label(s)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/CreateTerm
        ///     [{
	    ///         "TermStore":"c2bcfd2c8e7f4226a89b21a5d2a0d6d6",
	    ///         "TermGroup":"Sesam",
	    ///         "Termset":"AEN Prosjekter",
	    ///         "Term":"Childterming for Pipe",
	    ///         "Lcid": 1033,
	    ///         "children": {
	    ///         	"aTermName": "another child",
	    ///         	"bTermName": "another child in child",
	    ///         	"cTermName": "Sverres tre",
	    ///         	"dTermName": "Sverres tre2",
	    ///         	"eTermName": "Sverres tre5"
	    ///         },
	    ///         "Labels": [
	    ///         	{
	    ///         	"isDefaultForLanguage": false,
	    ///         	"language": 1033,
	    ///         	"value": "Test23"
	    ///         	},
	    ///         	{
	    ///         	"isDefaultForLanguage": false,
	    ///         	"language": 1033,
	    ///         	"value": "Test21231111111"
	    ///         	},
	    ///         	{
	    ///         	"isDefaultForLanguage": false,
	    ///         	"language": 1033,
	    ///         	"value": "Test2123123123"
	    ///         	}
	    ///         ]
        ///     }]
        ///     
        /// </remarks>
        /// <param name="param">New document parameters</param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<TaxonomyModel>>> CreateTerm(TaxonomyModel[] listT)
        {
            return await this._spTermsService.CreateTerm(listT);
        }


        // GET api/termset d
        [HttpGet("terms")]
        [Produces("application/json")]
        /// <summary>
        /// Fetch all Terms
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/getallterms?termstore=<termstore GUID>&termgroup=<termgroup name>&termset=<termset name>
        ///     
        /// </remarks>
        public async Task<IActionResult> GetAllTerms([FromQuery(Name = "termstore")] string _termstore, [FromQuery(Name = "termgroup")] string _termgroup, [FromQuery(Name = "termset")] string _termset)
        {
            return await this._spTermsService.AllTerms(_termstore, _termgroup, _termset);
        }

        [HttpPost("children")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<childFromParentModel>>> PostChildTerms([FromBody] childFromParentModel[] termList) 
        {
            return await this._spTermsService.CreateFromParentList(termList);
        }


        [HttpPost("child/children")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<childFromChildModel>>> PostChildChildTerms([FromBody] childFromChildModel[] termList) 
        {
            return await this._spTermsService.CreateFromChildList(termList);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<TermModel>>> PostTerms([FromBody] TermModel[] termList) 
        {
            return await this._spTermsService.CreateFromList(termList);
        }

        [HttpPost("child/child/children")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<childFromChildrenModel>>> PostChildChildChildrenTerms([FromBody] childFromChildrenModel[] termList) 
        {
            return await this._spTermsService.CreateFromChildChildList(termList);
        }

        [HttpPost("child/child/grandchild")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<grandchildFromChildrenModel>>> PostGrandChildTerms([FromBody] grandchildFromChildrenModel[] termList) 
        {
            return await this._spTermsService.CreateFromGrandchildList(termList);
        }

        [HttpPost("child/grandchildren/grandchild")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<grandchildFromChildChildModel>>> PostGrandChildChildTerms([FromBody] grandchildFromChildChildModel[] termList) 
        {
            return await this._spTermsService.CreateFromGrandchildChildList(termList);
        }
    }
}
