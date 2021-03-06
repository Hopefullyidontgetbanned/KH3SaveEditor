﻿using KHSave.Lib2.Types;
using KHSave.SaveEditor.Common;
using KHSave.SaveEditor.Common.Models;
using KHSave.SaveEditor.Kh2.Service;
using System;
using System.Collections.Generic;
using System.Windows;
using Xe.Tools;

namespace KHSave.SaveEditor.Kh2.ViewModels
{
    public class EquipmentItemsViewModel
    {
        private readonly IEquipmentManager equipmentManager;

        public class EquipmentItemViewModel : BaseNotifyPropertyChanged
        {
            private readonly EquipmentItemsViewModel _vm;
            private readonly uint _index;

            internal EquipmentItemViewModel(EquipmentItemsViewModel vm, uint index)
            {
                _vm = vm;
                _index = index;
                ValueSet = new KhEnumListModel<EnumIconTypeModel<EquipmentType>, EquipmentType>(() => Equipment, value => Equipment = value);
            }

            public Visibility AdvancedVisibility => Global.IsAdvancedMode ? Visibility.Visible : Visibility.Collapsed;

            public bool Enabled
            {
                get => _vm.IsEnabled(_index);
                set => _vm.SetEnabled(_index, value);
            }

            public EquipmentType Equipment
            {
                get => _vm.GetEquipment(_index);
                set => _vm.SetEquipment(_index, value);
            }

            public IEnumerable<EnumIconTypeModel<EquipmentType>> ValueSet { get; }

            public void InvalidateEnabled()
            {
                OnPropertyChanged(nameof(Enabled));
            }
        }


        public EquipmentItemsViewModel(IEquipmentManager equipmentManager)
        {
            this.equipmentManager = equipmentManager;
            Equipment1 = new EquipmentItemViewModel(this, 0);
            Equipment2 = new EquipmentItemViewModel(this, 1);
            Equipment3 = new EquipmentItemViewModel(this, 2);
            Equipment4 = new EquipmentItemViewModel(this, 3);
            Equipment5 = new EquipmentItemViewModel(this, 4);
            Equipment6 = new EquipmentItemViewModel(this, 5);
            Equipment7 = new EquipmentItemViewModel(this, 6);
            Equipment8 = new EquipmentItemViewModel(this, 7);
        }

        public EquipmentItemViewModel Equipment1 { get; private set; }
        public EquipmentItemViewModel Equipment2 { get; private set; }
        public EquipmentItemViewModel Equipment3 { get; private set; }
        public EquipmentItemViewModel Equipment4 { get; private set; }
        public EquipmentItemViewModel Equipment5 { get; private set; }
        public EquipmentItemViewModel Equipment6 { get; private set; }
        public EquipmentItemViewModel Equipment7 { get; private set; }
        public EquipmentItemViewModel Equipment8 { get; private set; }

        public bool IsEnabled(uint index) => equipmentManager.Count > index;
        public void SetEnabled(uint index, bool enabled)
        {
            if (enabled)
            {
                equipmentManager.Count = Math.Max(equipmentManager.Count, index + 1);
            }
            else
            {
                equipmentManager.Count = Math.Min(equipmentManager.Count, index);
            }

            Equipment1.InvalidateEnabled();
            Equipment2.InvalidateEnabled();
            Equipment3.InvalidateEnabled();
            Equipment4.InvalidateEnabled();
            Equipment5.InvalidateEnabled();
            Equipment6.InvalidateEnabled();
            Equipment7.InvalidateEnabled();
            Equipment8.InvalidateEnabled();
        }
        public EquipmentType GetEquipment(uint index) => equipmentManager.GetEquipment(index);
        public void SetEquipment(uint index, EquipmentType equipment) => equipmentManager.SetEquipment(index, equipment);
    }
}
