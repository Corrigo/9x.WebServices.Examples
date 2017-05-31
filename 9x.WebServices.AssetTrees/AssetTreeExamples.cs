using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.AssetTrees.Operations;


namespace _9x.WebServices.AssetTrees
{
    public static class AssetTreeExamples
    {

        public static void CRUDExample(CorrigoService service)
        {
            if (service == null) return;

            
            AssetTree assetTree = Create.Execute(service); // AssetTree is Readonly - NonCreatable, NonDeletable, NonUpdatable

            var assetTrees = Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

            if (assetTrees != null && assetTrees.Length > 0)
            {
                AssetTree lastAsset = (AssetTree) assetTrees[assetTrees.Length - 1];

                assetTree = Read.Retrieve(service, lastAsset.ChildId, lastAsset.ParentId);

                Update.Execute(service, ((AssetTree)assetTrees[assetTrees.Length - 1])); // AssetTree is Readonly - NonCreatable, NonDeletable, NonUpdatable

                Delete.Execute(service, assetTrees[assetTrees.Length - 1].Id); // AssetTree is Readonly - NonCreatable, NonDeletable, NonUpdatable
            }



        }
    }

}
