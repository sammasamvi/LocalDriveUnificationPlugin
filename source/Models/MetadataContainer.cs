using IUnification.Models.Enums;
using IUnification.Models.Interfaces;
using System;

namespace LocalDriveUnificationPlugin.Models
{
    internal sealed class MetadataContainer : IMetadataContainer
    {
        private string          _Album;
        private string          _Artist;
        private string          _StringDuration;
        private readonly string _FilePath;
        private TimeSpan        _TimeSpanDuration;
        private static string[] _MetadataFields = new string[] 
        {
            "Album",
            "Artist",
            "Duration",
            "Title"
        };
        private string          _Title;

        public Uri Datasource
        {
            get 
            { 
                return new Uri(_FilePath, UriKind.Absolute); 
            }
        }

        public DatasourceFormat DatasourceFormat
        {
            get 
            {
                return DatasourceFormat.Local;
            }
        }

        public TimeSpan Duration
        {
            get 
            {
                return _TimeSpanDuration;
            }
        }

        private void GenerateMetadata()
        {

        }

        public string Metadata(string MetadataField)
        {
            switch (MetadataField)
            {
                case "Album":
                    return _Album;

                case "Artist":
                    return _Artist;

                case "Duration":
                    return _StringDuration;

                case "Title":
                    return _Title;

                default:
                    return "";
            }
        }

        public MetadataContainer(String FilePath)
        {
            _FilePath = FilePath;
            GenerateMetadata();
        }

        public string[] MetadataFields
        {
            get 
            {
                return MetadataContainer._MetadataFields;
            }
        }
    }
}
