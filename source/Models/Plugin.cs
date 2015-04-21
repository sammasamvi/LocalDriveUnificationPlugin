using DirectoryScanner.Common;
using DirectoryScanner.Synchronous;
using IUnification.Models;
using IUnification.Models.Enums;
using IUnification.Models.Interfaces;
using System;
using System.ComponentModel;

namespace LocalDriveUnificationPlugin.Models
{
    public sealed class Plugin : IUnificationPlugin, IConfigurableUnificationPlugin
    {
        BindingList<IMetadataContainer> _MetadataCollection;
        InputField[]                    _InputFields;

        public string Author
        {
            get 
            { 
                return @"snwmelt"; 
            }
        }

        public void Dispose()
        {
            MetadataCollection.Clear();
        }

        public void CreateInputFields()
        {
            InputFields[0] = new InputField(@"Scan Directory", FieldType.Standard);
            InputFields[1] = new InputField(@"File Extensions", FieldType.Standard);
        }

        public InputField[] InputFields
        {
            set
            {
                _InputFields = value;
            }

            get
            {
                return _InputFields;
            }
        }

        public BindingList<IMetadataContainer> MetadataCollection
        {
            private set
            {
                _MetadataCollection = value;
            }

            get 
            { 
                return _MetadataCollection; 
            }
        }

        public event EventHandler<LoadingCompletedEventArgs> MetadataCollectionUpdatedEvent;

        public Plugin()
        {
            InputFields        = new InputField[2];
            MetadataCollection = new BindingList<IMetadataContainer>();
            
            CreateInputFields();
        }

        public PropertyContainer[] Properties
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get 
            { 
                return @"Local Drive Plugin"; 
            }
        }

        public void UpdateMetadataCollection()
        {
            Scanner _Scanner = new Scanner(InputFields[0].Value, true, ScanMode.MatchExtension, InputFields[1].Value.Split(','));
            _Scanner.FileFoundEvent += ((sender, FileInfo) => 
            {
                IMetadataContainer NewMetadataContainer = new MetadataContainer(FileInfo.FullName);
                MetadataCollection.Add(NewMetadataContainer);
            });

            _Scanner.Scan();

            if (MetadataCollectionUpdatedEvent != null)
                MetadataCollectionUpdatedEvent(this, new LoadingCompletedEventArgs(LoadingState.Complete));
        }

        public string Version
        {
            get 
            { 
                return @"0.0.1"; 
            }
        }
    }
}
