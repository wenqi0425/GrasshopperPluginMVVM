using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using SampleMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleViewModels
{
    public class DomInputModel: BaseDomInputModel
    {
        private WebView2 _webView;

        private List<DomInputModel> _domInputModels;

        // properties inheritanced from BaseDomInputModel
        public List<string> InputValues 
            => _domInputModels?.Select(s => s.value).ToList();
        
        public List<string> InputIds 
            => _domInputModels?.Select(s => s.id).ToList();

        public List<string> InputNames 
            => _domInputModels?.Select(s => s.name).ToList();

        public List<string> InputTypes 
            => _domInputModels?.Select(s => s.type).ToList();


        // Run the DOM Query script (JS) to get all the input elements.
        public async Task RunDomInputQuery()
        {
            string scriptResult = await _webView.ExecuteScriptAsync("queryInputElements();");
            dynamic deserializedDomModels = JsonConvert.DeserializeObject(scriptResult);
            if (deserializedDomModels == null) return;

            _domInputModels = new List<DomInputModel>();
            foreach (dynamic s in deserializedDomModels)
            {
                DomInputModel domInputModel = JsonConvert.DeserializeObject<DomInputModel>(s.ToString());
                _domInputModels.Add(domInputModel);
            }
        }
    }
}
