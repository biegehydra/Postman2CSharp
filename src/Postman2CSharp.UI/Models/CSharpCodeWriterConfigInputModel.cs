using System.ComponentModel;
using Xamasoft.JsonClassGenerator.CodeWriterConfiguration;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.UI.Models
{
    public partial class CSharpCodeWriterConfigInputModel : INotifyPropertyChanged
    {
        private OutputCollectionType _collectionType;
        public OutputCollectionType CollectionType
        {
            get => _collectionType;
            set
            {
                if (_collectionType != value)
                {
                    _collectionType = value;
                    OnPropertyChanged(nameof(CollectionType));
                }
            }
        }

        private bool _usePascalCase;
        public bool UsePascalCase
        {
            get => _usePascalCase;
            set
            {
                if (_usePascalCase != value)
                {
                    _usePascalCase = value;
                    OnPropertyChanged(nameof(UsePascalCase));
                }
            }
        }

        private bool _useNestedClasses;
        public bool UseNestedClasses
        {
            get => _useNestedClasses;
            set
            {
                if (_useNestedClasses != value)
                {
                    _useNestedClasses = value;
                    OnPropertyChanged(nameof(UseNestedClasses));
                }
            }
        }

        private JsonLibrary _attributeLibrary;
        public JsonLibrary AttributeLibrary
        {
            get => _attributeLibrary;
            set
            {
                if (_attributeLibrary != value)
                {
                    _attributeLibrary = value;
                    OnPropertyChanged(nameof(AttributeLibrary));
                }
            }
        }

        private bool _nullValueHandlingIgnore;
        public bool NullValueHandlingIgnore
        {
            get => _nullValueHandlingIgnore;
            set
            {
                if (_nullValueHandlingIgnore != value)
                {
                    _nullValueHandlingIgnore = value;
                    OnPropertyChanged(nameof(NullValueHandlingIgnore));
                }
            }
        }

        private JsonPropertyAttributeUsage _attributeUsage;
        public JsonPropertyAttributeUsage AttributeUsage
        {
            get => _attributeUsage;
            set
            {
                if (_attributeUsage != value)
                {
                    _attributeUsage = value;
                    OnPropertyChanged(nameof(AttributeUsage));
                }
            }
        }

        private OutputTypes _outputType;
        public OutputTypes OutputType
        {
            get => _outputType;
            set
            {
                if (_outputType != value)
                {
                    _outputType = value;
                    OnPropertyChanged(nameof(OutputType));
                }
            }
        }

        private OutputMembers _outputMembers;
        public OutputMembers OutputMembers
        {
            get => _outputMembers;
            set
            {
                if (_outputMembers != value)
                {
                    _outputMembers = value;
                    OnPropertyChanged(nameof(OutputMembers));
                }
            }
        }

        private bool _readOnlyCollectionProperties;
        public bool ReadOnlyCollectionProperties
        {
            get => _readOnlyCollectionProperties;
            set
            {
                if (_readOnlyCollectionProperties != value)
                {
                    _readOnlyCollectionProperties = value;
                    OnPropertyChanged(nameof(ReadOnlyCollectionProperties));
                }
            }
        }

        private bool _alwaysUseNullables;
        public bool AlwaysUseNullables
        {
            get => _alwaysUseNullables;
            set
            {
                if (_alwaysUseNullables != value)
                {
                    _alwaysUseNullables = value;
                    OnPropertyChanged(nameof(AlwaysUseNullables));
                }
            }
        }

        public CSharpCodeWriterConfig ToCSharpCodeWriterConfig()
        {
            return new CSharpCodeWriterConfig(
                UsePascalCase,
                UseNestedClasses,
                AttributeLibrary,
                AttributeUsage,
                OutputType,
                OutputMembers,
                ReadOnlyCollectionProperties,
                CollectionType,
                AlwaysUseNullables,
                NullValueHandlingIgnore);
        }

        public bool Equals(CSharpCodeWriterConfigInputModel other)
        {
            return UsePascalCase == other.UsePascalCase &&
                   UseNestedClasses == other.UseNestedClasses &&
                   AttributeLibrary == other.AttributeLibrary &&
                   AttributeUsage == other.AttributeUsage &&
                   OutputType == other.OutputType &&
                   OutputMembers == other.OutputMembers &&
                   ReadOnlyCollectionProperties == other.ReadOnlyCollectionProperties &&
                   CollectionType == other.CollectionType &&
                   AlwaysUseNullables == other.AlwaysUseNullables &&
                   NullValueHandlingIgnore == other.NullValueHandlingIgnore;
        }

        public CSharpCodeWriterConfigInputModel Clone()
        {
            return (CSharpCodeWriterConfigInputModel)MemberwiseClone();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
