﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectricCarGUI.ElectricCarService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Booking", Namespace="http://schemas.datacontract.org/2004/07/ElectricCarWCF")]
    [System.SerializableAttribute()]
    public partial class Booking : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ElectricCarGUI.ElectricCarService.BookingLine[] bookinglinesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int cIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string createDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ElectricCarGUI.ElectricCarService.CustomerTest customerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string payStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int startStationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal totalPriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string tripStartField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ElectricCarGUI.ElectricCarService.BookingLine[] bookinglines {
            get {
                return this.bookinglinesField;
            }
            set {
                if ((object.ReferenceEquals(this.bookinglinesField, value) != true)) {
                    this.bookinglinesField = value;
                    this.RaisePropertyChanged("bookinglines");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int cId {
            get {
                return this.cIdField;
            }
            set {
                if ((this.cIdField.Equals(value) != true)) {
                    this.cIdField = value;
                    this.RaisePropertyChanged("cId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string createDate {
            get {
                return this.createDateField;
            }
            set {
                if ((object.ReferenceEquals(this.createDateField, value) != true)) {
                    this.createDateField = value;
                    this.RaisePropertyChanged("createDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ElectricCarGUI.ElectricCarService.CustomerTest customer {
            get {
                return this.customerField;
            }
            set {
                if ((object.ReferenceEquals(this.customerField, value) != true)) {
                    this.customerField = value;
                    this.RaisePropertyChanged("customer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string payStatus {
            get {
                return this.payStatusField;
            }
            set {
                if ((object.ReferenceEquals(this.payStatusField, value) != true)) {
                    this.payStatusField = value;
                    this.RaisePropertyChanged("payStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int startStationId {
            get {
                return this.startStationIdField;
            }
            set {
                if ((this.startStationIdField.Equals(value) != true)) {
                    this.startStationIdField = value;
                    this.RaisePropertyChanged("startStationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal totalPrice {
            get {
                return this.totalPriceField;
            }
            set {
                if ((this.totalPriceField.Equals(value) != true)) {
                    this.totalPriceField = value;
                    this.RaisePropertyChanged("totalPrice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string tripStart {
            get {
                return this.tripStartField;
            }
            set {
                if ((object.ReferenceEquals(this.tripStartField, value) != true)) {
                    this.tripStartField = value;
                    this.RaisePropertyChanged("tripStart");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CustomerTest", Namespace="http://schemas.datacontract.org/2004/07/ElectricCarWCF")]
    [System.SerializableAttribute()]
    public partial class CustomerTest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BookingLine", Namespace="http://schemas.datacontract.org/2004/07/ElectricCarWCF")]
    [System.SerializableAttribute()]
    public partial class BookingLine : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ElectricCarGUI.ElectricCarService.BatteryTypeTest BatteryTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal priceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int quantityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ElectricCarGUI.ElectricCarService.Station stationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime timeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ElectricCarGUI.ElectricCarService.BatteryTypeTest BatteryType {
            get {
                return this.BatteryTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.BatteryTypeField, value) != true)) {
                    this.BatteryTypeField = value;
                    this.RaisePropertyChanged("BatteryType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal price {
            get {
                return this.priceField;
            }
            set {
                if ((this.priceField.Equals(value) != true)) {
                    this.priceField = value;
                    this.RaisePropertyChanged("price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int quantity {
            get {
                return this.quantityField;
            }
            set {
                if ((this.quantityField.Equals(value) != true)) {
                    this.quantityField = value;
                    this.RaisePropertyChanged("quantity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ElectricCarGUI.ElectricCarService.Station station {
            get {
                return this.stationField;
            }
            set {
                if ((object.ReferenceEquals(this.stationField, value) != true)) {
                    this.stationField = value;
                    this.RaisePropertyChanged("station");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime time {
            get {
                return this.timeField;
            }
            set {
                if ((this.timeField.Equals(value) != true)) {
                    this.timeField = value;
                    this.RaisePropertyChanged("time");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BatteryTypeTest", Namespace="http://schemas.datacontract.org/2004/07/ElectricCarWCF")]
    [System.SerializableAttribute()]
    public partial class BatteryTypeTest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Station", Namespace="http://schemas.datacontract.org/2004/07/ElectricCarWCF")]
    [System.SerializableAttribute()]
    public partial class Station : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="NaborStation", Namespace="http://schemas.datacontract.org/2004/07/ElectricCarWCF")]
    [System.SerializableAttribute()]
    public partial class NaborStation : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double DistanceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double DriveHourField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Distance {
            get {
                return this.DistanceField;
            }
            set {
                if ((this.DistanceField.Equals(value) != true)) {
                    this.DistanceField = value;
                    this.RaisePropertyChanged("Distance");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double DriveHour {
            get {
                return this.DriveHourField;
            }
            set {
                if ((this.DriveHourField.Equals(value) != true)) {
                    this.DriveHourField = value;
                    this.RaisePropertyChanged("DriveHour");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ElectricCarService.IElectricCar")]
    public interface IElectricCar {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getME", ReplyAction="http://tempuri.org/IElectricCar/getMEResponse")]
        string getME();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getME", ReplyAction="http://tempuri.org/IElectricCar/getMEResponse")]
        System.Threading.Tasks.Task<string> getMEAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getAllBookings", ReplyAction="http://tempuri.org/IElectricCar/getAllBookingsResponse")]
        ElectricCarGUI.ElectricCarService.Booking[] getAllBookings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getAllBookings", ReplyAction="http://tempuri.org/IElectricCar/getAllBookingsResponse")]
        System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Booking[]> getAllBookingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getBooking", ReplyAction="http://tempuri.org/IElectricCar/getBookingResponse")]
        ElectricCarGUI.ElectricCarService.Booking getBooking(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getBooking", ReplyAction="http://tempuri.org/IElectricCar/getBookingResponse")]
        System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Booking> getBookingAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getAllStations", ReplyAction="http://tempuri.org/IElectricCar/getAllStationsResponse")]
        ElectricCarGUI.ElectricCarService.Station[] getAllStations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getAllStations", ReplyAction="http://tempuri.org/IElectricCar/getAllStationsResponse")]
        System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Station[]> getAllStationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/addStation", ReplyAction="http://tempuri.org/IElectricCar/addStationResponse")]
        void addStation(string name, string address, string country, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/addStation", ReplyAction="http://tempuri.org/IElectricCar/addStationResponse")]
        System.Threading.Tasks.Task addStationAsync(string name, string address, string country, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getStation", ReplyAction="http://tempuri.org/IElectricCar/getStationResponse")]
        ElectricCarGUI.ElectricCarService.Station getStation(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getStation", ReplyAction="http://tempuri.org/IElectricCar/getStationResponse")]
        System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Station> getStationAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/updateStation", ReplyAction="http://tempuri.org/IElectricCar/updateStationResponse")]
        void updateStation(int id, string name, string address, string country, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/updateStation", ReplyAction="http://tempuri.org/IElectricCar/updateStationResponse")]
        System.Threading.Tasks.Task updateStationAsync(int id, string name, string address, string country, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/deleteStation", ReplyAction="http://tempuri.org/IElectricCar/deleteStationResponse")]
        void deleteStation(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/deleteStation", ReplyAction="http://tempuri.org/IElectricCar/deleteStationResponse")]
        System.Threading.Tasks.Task deleteStationAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getNaborStations", ReplyAction="http://tempuri.org/IElectricCar/getNaborStationsResponse")]
        ElectricCarGUI.ElectricCarService.NaborStation[] getNaborStations(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getNaborStations", ReplyAction="http://tempuri.org/IElectricCar/getNaborStationsResponse")]
        System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.NaborStation[]> getNaborStationsAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/addNaborStation", ReplyAction="http://tempuri.org/IElectricCar/addNaborStationResponse")]
        void addNaborStation(int id1, int id2, decimal distance, decimal drivehour);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/addNaborStation", ReplyAction="http://tempuri.org/IElectricCar/addNaborStationResponse")]
        System.Threading.Tasks.Task addNaborStationAsync(int id1, int id2, decimal distance, decimal drivehour);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/updateNaborStation", ReplyAction="http://tempuri.org/IElectricCar/updateNaborStationResponse")]
        void updateNaborStation(int id1, int id2, decimal distance, decimal driveHour);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/updateNaborStation", ReplyAction="http://tempuri.org/IElectricCar/updateNaborStationResponse")]
        System.Threading.Tasks.Task updateNaborStationAsync(int id1, int id2, decimal distance, decimal driveHour);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/deleteNaborStation", ReplyAction="http://tempuri.org/IElectricCar/deleteNaborStationResponse")]
        void deleteNaborStation(int id1, int id2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/deleteNaborStation", ReplyAction="http://tempuri.org/IElectricCar/deleteNaborStationResponse")]
        System.Threading.Tasks.Task deleteNaborStationAsync(int id1, int id2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getStates", ReplyAction="http://tempuri.org/IElectricCar/getStatesResponse")]
        string[] getStates();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IElectricCar/getStates", ReplyAction="http://tempuri.org/IElectricCar/getStatesResponse")]
        System.Threading.Tasks.Task<string[]> getStatesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IElectricCarChannel : ElectricCarGUI.ElectricCarService.IElectricCar, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ElectricCarClient : System.ServiceModel.ClientBase<ElectricCarGUI.ElectricCarService.IElectricCar>, ElectricCarGUI.ElectricCarService.IElectricCar {
        
        public ElectricCarClient() {
        }
        
        public ElectricCarClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ElectricCarClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ElectricCarClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ElectricCarClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string getME() {
            return base.Channel.getME();
        }
        
        public System.Threading.Tasks.Task<string> getMEAsync() {
            return base.Channel.getMEAsync();
        }
        
        public ElectricCarGUI.ElectricCarService.Booking[] getAllBookings() {
            return base.Channel.getAllBookings();
        }
        
        public System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Booking[]> getAllBookingsAsync() {
            return base.Channel.getAllBookingsAsync();
        }
        
        public ElectricCarGUI.ElectricCarService.Booking getBooking(int id) {
            return base.Channel.getBooking(id);
        }
        
        public System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Booking> getBookingAsync(int id) {
            return base.Channel.getBookingAsync(id);
        }
        
        public ElectricCarGUI.ElectricCarService.Station[] getAllStations() {
            return base.Channel.getAllStations();
        }
        
        public System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Station[]> getAllStationsAsync() {
            return base.Channel.getAllStationsAsync();
        }
        
        public void addStation(string name, string address, string country, string state) {
            base.Channel.addStation(name, address, country, state);
        }
        
        public System.Threading.Tasks.Task addStationAsync(string name, string address, string country, string state) {
            return base.Channel.addStationAsync(name, address, country, state);
        }
        
        public ElectricCarGUI.ElectricCarService.Station getStation(int id) {
            return base.Channel.getStation(id);
        }
        
        public System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.Station> getStationAsync(int id) {
            return base.Channel.getStationAsync(id);
        }
        
        public void updateStation(int id, string name, string address, string country, string state) {
            base.Channel.updateStation(id, name, address, country, state);
        }
        
        public System.Threading.Tasks.Task updateStationAsync(int id, string name, string address, string country, string state) {
            return base.Channel.updateStationAsync(id, name, address, country, state);
        }
        
        public void deleteStation(int id) {
            base.Channel.deleteStation(id);
        }
        
        public System.Threading.Tasks.Task deleteStationAsync(int id) {
            return base.Channel.deleteStationAsync(id);
        }
        
        public ElectricCarGUI.ElectricCarService.NaborStation[] getNaborStations(int id) {
            return base.Channel.getNaborStations(id);
        }
        
        public System.Threading.Tasks.Task<ElectricCarGUI.ElectricCarService.NaborStation[]> getNaborStationsAsync(int id) {
            return base.Channel.getNaborStationsAsync(id);
        }
        
        public void addNaborStation(int id1, int id2, decimal distance, decimal drivehour) {
            base.Channel.addNaborStation(id1, id2, distance, drivehour);
        }
        
        public System.Threading.Tasks.Task addNaborStationAsync(int id1, int id2, decimal distance, decimal drivehour) {
            return base.Channel.addNaborStationAsync(id1, id2, distance, drivehour);
        }
        
        public void updateNaborStation(int id1, int id2, decimal distance, decimal driveHour) {
            base.Channel.updateNaborStation(id1, id2, distance, driveHour);
        }
        
        public System.Threading.Tasks.Task updateNaborStationAsync(int id1, int id2, decimal distance, decimal driveHour) {
            return base.Channel.updateNaborStationAsync(id1, id2, distance, driveHour);
        }
        
        public void deleteNaborStation(int id1, int id2) {
            base.Channel.deleteNaborStation(id1, id2);
        }
        
        public System.Threading.Tasks.Task deleteNaborStationAsync(int id1, int id2) {
            return base.Channel.deleteNaborStationAsync(id1, id2);
        }
        
        public string[] getStates() {
            return base.Channel.getStates();
        }
        
        public System.Threading.Tasks.Task<string[]> getStatesAsync() {
            return base.Channel.getStatesAsync();
        }
    }
}
