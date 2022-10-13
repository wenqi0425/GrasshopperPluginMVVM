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

        private DomInputModel _domInputModel;

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

        public async void RunDomInputQuery(DomClickModel clickModel)
        {
            await RunDomInputQuery();
            // handle output for buttons
            if (clickModel.targetType == "button")
            {
                HandleButtonClick(clickModel);
            }
        }

        private void HandleButtonClick(DomClickModel clickModel)
        {
            //if (clickModel.targetType != "button") return;
            // TODO: need to ensure that there is a unique id for each button, even when users
            // are not using the id/name feature correctly. for now we loop over all the possible buttons
            var clickedButtons = 
                _domInputModels.Where(m 
                        => m.type == clickModel.targetType &&
                           m.id == clickModel.targetId ||
                           m.name == clickModel.targetName);

            //if (clickedButtons == null) return;
            foreach (DomInputModel domInput in clickedButtons)
            {
                domInput.value = "true";
            }
        }
    }
}
