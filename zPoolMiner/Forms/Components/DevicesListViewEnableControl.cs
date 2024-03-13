﻿/*
* This is an open source non-commercial project. Dear PVS-Studio, please check it.
* PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
*/
using zPoolMiner.Configs;
using zPoolMiner.Devices;
using zPoolMiner.Interfaces;
using zPoolMiner.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using zPoolMiner;

namespace zPoolMiner.Forms.Components
{
    public partial class DevicesListViewEnableControl : UserControl
    {
        private const int ENABLED = 0;
        private const int TEMP = 1;
        private const int LOAD = 2;
        private const int FAN = 3;
        private const int POWER = 4;
        public static Color EnabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
        //public static Color DisabledColor = ConfigManager.GeneralConfig.ColorProfileIndex != 0 ? Color.FromArgb(Form_Main._backColor.ToArgb() + 40 * 256 * 256 * 256 + 40 * 256 * 256 + 40 * 256 + 40) : Color.DarkGray;
        public static Color DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
        public static Color DisabledForeColor = Color.Gray;
        //public static Color DisabledColor = SystemColors.ControlLight;

        public class DefaultDevicesColorSeter : IListItemCheckColorSetter
        {

            public void LviSetColor(ListViewItem lvi)
            {
                if (lvi.Tag is ComputeDevice cdvo)
                {
                    
                        lvi.BackColor = cdvo.Enabled ? EnabledColor : DisabledColor;
                        lvi.ForeColor = cdvo.Enabled ? Form_Main._foreColor : DisabledForeColor;
                }
            }
        }

        private IListItemCheckColorSetter _listItemCheckColorSetter = new DefaultDevicesColorSeter();

        public IBenchmarkCalculation BenchmarkCalculation { get; set; }

        private AlgorithmsListView _algorithmsListView;

        // disable checkboxes when in benchmark mode
        private bool _isInBenchmark;

        // helper for benchmarking logic
        public bool IsInBenchmark
        {
            get => _isInBenchmark;
            set
            {
                if (value)
                {
                    _isInBenchmark = true;
                    listViewDevices.CheckBoxes = false;
                }
                else
                {
                    _isInBenchmark = false;
                    listViewDevices.CheckBoxes = true;
                }
            }
        }

        private bool _isMining;

        public bool IsMining
        {
            get => _isMining;
            set
            {
                if (value)
                {
                    _isMining = true;
                    listViewDevices.CheckBoxes = false;
                }
                else
                {
                    _isMining = false;
                    listViewDevices.CheckBoxes = true;
                }
            }
        }

        public bool IsBenchmarkForm = false;
        public bool IsSettingsCopyEnabled = false;

        public string FirstColumnText
        {
            get => listViewDevices.Columns[ENABLED].Text;
            set
            {
                if (value != null) listViewDevices.Columns[ENABLED].Text = value;
            }
        }


        public bool SaveToGeneralConfig { get; set; }


        public DevicesListViewEnableControl()
        {
            InitializeComponent();

            listViewDevices.DoubleBuffer();
            DevicesListViewEnableControl.colorListViewHeader(ref listViewDevices, EnabledColor, System.Drawing.Color.White);
            SaveToGeneralConfig = false;
            // intialize ListView callbacks
            listViewDevices.ItemChecked += ListViewDevicesItemChecked;
            this.listViewDevices.ItemCheck += new ItemCheckEventHandler(listViewDevices_ItemCheck);

            IsMining = false;
            BenchmarkCalculation = null;
          //  listViewDevices.OwnerDraw = true;
        }

        public void SetIListItemCheckColorSetter(IListItemCheckColorSetter listItemCheckColorSetter)
        {
            _listItemCheckColorSetter = listItemCheckColorSetter;
        }

        public void SetAlgorithmsListView(AlgorithmsListView algorithmsListView)
        {
            _algorithmsListView = algorithmsListView;
        }

        public void ResetListItemColors()
        {
            foreach (ListViewItem lvi in listViewDevices.Items)
            {
                _listItemCheckColorSetter?.LviSetColor(lvi);
            }
        }

        public void SetComputeDevices(List<ComputeDevice> computeDevices)
        {
            // to not run callbacks when setting new
            var tmpSaveToGeneralConfig = SaveToGeneralConfig;
            SaveToGeneralConfig = false;
            listViewDevices.BeginUpdate();
            listViewDevices.Items.Clear();
            string addInfo = "";
            // set devices
            foreach (var computeDevice in computeDevices)
            {
                if (computeDevice.DeviceType != DeviceType.CPU)
                {
                    addInfo = " (" + computeDevice.GpuRam / 1073741824 + " GB)";
                }
                var lvi = new ListViewItem
                {
                    Checked = computeDevice.Enabled,
                    Text = computeDevice.GetFullName() + addInfo,
                    Tag = computeDevice
                };
                //lvi.SubItems.Add(computeDevice.Name);
                listViewDevices.Items.Add(lvi);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                _listItemCheckColorSetter.LviSetColor(lvi);


            }
            int index = 0;
            foreach (var computeDevice in computeDevices)
            {
                string cTemp = Math.Truncate(computeDevice.Temp).ToString() + "°C";
                string cLoad = Math.Truncate(computeDevice.Load).ToString() + "%";
                string cFanSpeed = computeDevice.FanSpeed.ToString();
                if (computeDevice.Temp == 0)
                { cTemp = "N/A"; }
                    cFanSpeed += "%";

                string cPowerUsage = Math.Truncate(computeDevice.PowerUsage).ToString();
                if (Math.Truncate(computeDevice.PowerUsage) == 0)
                { 
                cPowerUsage = "-1";
                }

                cPowerUsage = cPowerUsage + " W";

                if (index >= 0)
                {
                    listViewDevices.Items[index].SubItems[1].Text = cTemp.Contains("-1") ? "--" : cTemp;
                    listViewDevices.Items[index].SubItems[2].Text = cLoad.Contains("-1") ? "--" : cLoad;
                    listViewDevices.Items[index].SubItems[3].Text = cFanSpeed.Contains("-1") ? "--" : cFanSpeed;
                    listViewDevices.Items[index].SubItems[4].Text = cPowerUsage.Contains("-1") ? "--" : cPowerUsage;
                }
                index++;
                /*Form_Main form = (Form_Main)ParentForm;

                if (computeDevice.Temp < ConfigManager.GeneralConfig.tempLowThreshold && computeDevice.Enabled && form.getDevicesListControl().IsMining && ConfigManager.GeneralConfig.beep)
                {
                    if (computeDevice.Temp == 0)
                    { }
                    else { Console.Beep(); }
                    //c = Color.LightBlue;
                }
                else if (computeDevice.Temp > ConfigManager.GeneralConfig.tempHighThreshold && computeDevice.Enabled && form.getDevicesListControl().IsMining && ConfigManager.GeneralConfig.beep)
                {
                    //c = Color.LightSalmon;
                    Console.Beep();
                }*/

            }

            listViewDevices.EndUpdate();
            listViewDevices.Invalidate(true);
            // reset properties
            SaveToGeneralConfig = tmpSaveToGeneralConfig;
        }

        public void SetComputeDevicesStatus(List<ComputeDevice> computeDevices)
        {
            int index = 0;
            foreach (var computeDevice in computeDevices)
            {
                string cTemp = Math.Truncate(computeDevice.Temp).ToString() + "°C";
                string cLoad = Math.Truncate(computeDevice.Load).ToString() + "%";
                string cFanSpeed = computeDevice.FanSpeed.ToString();
                
                    cFanSpeed += "%";
               
                    string cPowerUsage = Math.Truncate(computeDevice.PowerUsage).ToString();
                if (Math.Truncate(computeDevice.PowerUsage) == 0)
                
                    cPowerUsage = "-1";
                
                
                    cPowerUsage = cPowerUsage + " W";
                
                if (index >= 0)
                {
                    listViewDevices.Items[index].SubItems[1].Text = cTemp.Contains("-1") ? "--" : cTemp;
                    listViewDevices.Items[index].SubItems[2].Text = cLoad.Contains("-1") ? "--" : cLoad;
                    listViewDevices.Items[index].SubItems[3].Text = cFanSpeed.Contains("-1") ? "--" : cFanSpeed;
                    listViewDevices.Items[index].SubItems[4].Text = cPowerUsage.Contains("-1") ? "--" : cPowerUsage;
                }
                index++;

            }
        }
        public void ResetComputeDevices(List<ComputeDevice> computeDevices)
        {
            SetComputeDevices(computeDevices);
        }
        //List view header formatters
        public static void colorListViewHeader(ref ListView list, Color backColor, Color foreColor)
        {

            list.OwnerDraw = true;
            list.DrawColumnHeader +=
            new DrawListViewColumnHeaderEventHandler
            (
            (sender, e) => headerDraw(sender, e, backColor, foreColor)
            );
            list.DrawItem += new DrawListViewItemEventHandler(bodyDraw);
            list.Columns[TEMP].TextAlign = HorizontalAlignment.Center; //не работает

        }

        private static void headerDraw(object sender, DrawListViewColumnHeaderEventArgs e, Color backColor, Color foreColor)
        {
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            {
                StringFormat sf = new StringFormat();
                if ((e.ColumnIndex == 0))
                {
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                }
                else
                {
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                }
                e.Graphics.DrawString(e.Header.Text, e.Font, foreBrush, e.Bounds, sf);
            }
        }

        private static void bodyDraw(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
            //lvi.BackColor = cdvo.Enabled ? EnabledColor : DisabledColor;
            
                using (SolidBrush backBrush = new SolidBrush(SystemColors.ControlLightLight))
                {
                    e.Graphics.FillRectangle(backBrush, e.Bounds);
                }

        }
        public void InitLocale()
        {

            var _backColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            var _foreColor = System.Drawing.Color.White;
            var _textColor = System.Drawing.Color.White;


            foreach (var lbl in this.Controls.OfType<ListView>()) lbl.BackColor = SystemColors.ControlLightLight;
                listViewDevices.BackColor = _backColor;
                listViewDevices.ForeColor = _textColor;

                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));

            listViewDevices.Columns[ENABLED].Text = " " + International.GetText("ListView_Device");

            if (ConfigManager.GeneralConfig.Language == LanguageType.Ru)
            {
                listViewDevices.Columns[TEMP].Text = "Температура";
                listViewDevices.Columns[LOAD].Text = "Нагрузка";
                listViewDevices.Columns[FAN].Text = "Об/мин";
                listViewDevices.Columns[POWER].Text = "Потребление";
            } else
            {
                listViewDevices.Columns[TEMP].Text = "Temp";
                listViewDevices.Columns[LOAD].Text = "Load";
                listViewDevices.Columns[FAN].Text = "Fan";
                listViewDevices.Columns[POWER].Text = "Power";
            }
            listViewDevices.Columns[TEMP].Width = 78;
            listViewDevices.Columns[TEMP].TextAlign = HorizontalAlignment.Center; //не работает
            listViewDevices.Columns[LOAD].Width = 65;
            listViewDevices.Columns[FAN].Width = 59;
            listViewDevices.Columns[POWER].Width = 110;
            //listViewDevices.Columns[0].Width = Width - 4 - SystemInformation.VerticalScrollBarWidth;
            //listViewDevices.Columns[0].Width = Width - SystemInformation.VerticalScrollBarWidth;
            listViewDevices.Columns[0].Width = Width - 316;

        }

        public void InitLocaleMain()
        {
            var _backColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            var _foreColor = System.Drawing.Color.White;
            var _textColor = System.Drawing.Color.White;
            // foreach (var lbl in this.Controls.OfType<ListView>()) lbl.BackColor = _backColor;
            //  listViewDevices.BackColor = _backColor;
            // listViewDevices.ForeColor = _textColor;
            // this.BackColor = _backColor;
            
                foreach (var lbl in this.Controls.OfType<ListView>()) lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
                listViewDevices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
                listViewDevices.ForeColor = System.Drawing.Color.White;

                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));


            listViewDevices.Columns[ENABLED].Text = " " + International.GetText("ListView_Device");
            if (ConfigManager.GeneralConfig.Language == LanguageType.Ru)
            {
                listViewDevices.Columns[TEMP].Text = "Температура";
                listViewDevices.Columns[LOAD].Text = "Нагрузка";
                listViewDevices.Columns[FAN].Text = "Об/мин";
                listViewDevices.Columns[POWER].Text = "Потребление";
            } else
            {
                listViewDevices.Columns[TEMP].Text = "Temp";
                listViewDevices.Columns[LOAD].Text = "Load";
                listViewDevices.Columns[FAN].Text = "Fan";
                listViewDevices.Columns[POWER].Text = "Power";
            }

            listViewDevices.Columns[ENABLED].Width = 388;
            listViewDevices.Columns[TEMP].Width = 78;
            listViewDevices.Columns[LOAD].Width = 65;
            listViewDevices.Columns[FAN].Width = 59;
            listViewDevices.Columns[POWER].Width = 110;
            //  listViewDevices.Scrollable = true;
        }
        public void SaveColumns()
        {
           // if (listViewDevices.Columns[ENABLED] != null)
            if (listViewDevices.Columns[TEMP].Width + listViewDevices.Columns[LOAD].Width + listViewDevices.Columns[FAN].Width + listViewDevices.Columns[POWER].Width > 0)
            {
                ConfigManager.GeneralConfig.ColumnENABLED = listViewDevices.Columns[ENABLED].Width;
                ConfigManager.GeneralConfig.ColumnTEMP = listViewDevices.Columns[TEMP].Width;
                ConfigManager.GeneralConfig.ColumnLOAD = listViewDevices.Columns[LOAD].Width;
                ConfigManager.GeneralConfig.ColumnFAN = listViewDevices.Columns[FAN].Width;
                ConfigManager.GeneralConfig.ColumnPOWER = listViewDevices.Columns[POWER].Width;
            }
        }

        private void ListViewDevicesItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is ComputeDevice cDevice)
            {
                cDevice.Enabled = e.Item.Checked;

                if (SaveToGeneralConfig)
                {
                    ConfigManager.GeneralConfigFileCommit();
                }
                if (e.Item is ListViewItem lvi) _listItemCheckColorSetter.LviSetColor(lvi);
                _algorithmsListView?.RepaintStatus(cDevice.Enabled, cDevice.UUID);
            }
            BenchmarkCalculation?.CalcBenchmarkDevicesAlgorithmQueue();
        }

        public void SetDeviceSelectionChangedCallback(ListViewItemSelectionChangedEventHandler callback)
        {
            listViewDevices.ItemSelectionChanged += callback;
        }

        private void ListViewDevices_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsInBenchmark) return;
            if (IsMining) return;
            if (e.Button == MouseButtons.Right)
            {
                if (listViewDevices.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Items.Clear();
                    if (IsSettingsCopyEnabled)
                    {
                        if (listViewDevices.FocusedItem.Tag is ComputeDevice cDevice)
                        {
                            var sameDevTypes =
                                ComputeDeviceManager.Avaliable.GetSameDevicesTypeAsDeviceWithUUID(cDevice.UUID);
                            if (sameDevTypes.Count > 0)
                            {
                                var copyBenchItem = new ToolStripMenuItem();
                                var copyTuningItem = new ToolStripMenuItem();
                                //copyBenchItem.DropDownItems
                                foreach (var cDev in sameDevTypes)
                                {
                                    if (cDev.Enabled)
                                    {
                                        var copyBenchDropDownItem = new ToolStripMenuItem
                                        {
                                            Text = "GPU#" + cDev.Index.ToString() + " " + cDev.Name,
                                            Checked = cDev.UUID == cDevice.BenchmarkCopyUUID
                                        };
                                        copyBenchDropDownItem.Click += ToolStripMenuItemCopySettings_Click;
                                        copyBenchDropDownItem.Tag = cDev.UUID;
                                        copyBenchItem.DropDownItems.Add(copyBenchDropDownItem);

                                       /* var copyTuningDropDownItem = new ToolStripMenuItem
                                        {
                                            Text = "GPU#" + cDev.Index.ToString() + " " + cDev.Name
                                            //Checked = cDev.UUID == CDevice.TuningCopyUUID
                                        };*/
                                        //copyTuningDropDownItem.Click += ToolStripMenuItemCopyTuning_Click;
                                        //copyTuningDropDownItem.Tag = cDev.UUID;
                                        //copyTuningItem.DropDownItems.Add(copyTuningDropDownItem);
                                    }
                                }
                                copyBenchItem.Text = International.GetText("DeviceListView_ContextMenu_CopySettings");
                                //copyTuningItem.Text = International.GetText("DeviceListView_ContectMenu_CopyTuning");
                                contextMenuStrip1.Items.Add(copyBenchItem);
                                //contextMenuStrip1.Items.Add(copyTuningItem);
                            }
                        }
                    }
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, bool justTuning)
        {
            if (sender is ToolStripMenuItem item && item.Tag is string uuid
                && listViewDevices.FocusedItem.Tag is ComputeDevice CDevice)
            {
                var copyBenchCDev = ComputeDeviceManager.Avaliable.GetDeviceWithUUID(uuid);

                var result = MessageBox.Show(
                    string.Format(
                        International.GetText("DeviceListView_ContextMenu_CopySettings_Confirm_Dialog_Msg"),
                        copyBenchCDev.GetFullName(), CDevice.GetFullName()),
                    International.GetText("DeviceListView_ContextMenu_CopySettings_Confirm_Dialog_Title"),
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    
                        CDevice.BenchmarkCopyUUID = uuid;
                        CDevice.CopyBenchmarkSettingsFrom(copyBenchCDev);
                    
                    if (_algorithmsListView != null)
                    {
                        _algorithmsListView.Update();
                        _algorithmsListView.Refresh();
                        _algorithmsListView.RepaintStatus(CDevice.Enabled, CDevice.UUID);
                    }
                }
            }
        }

        private void ToolStripMenuItemCopySettings_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_Click(sender, false);
        }

        private void ToolStripMenuItemCopyTuning_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_Click(sender, true);
        }

        private void DevicesListViewEnableControl_Resize(object sender, EventArgs e)
        {
            //ResizeColumn();
            listViewDevices.BeginUpdate();
            ResizeAutoSizeColumn(listViewDevices, 0);
            listViewDevices.EndUpdate();
            // only one
            foreach (ColumnHeader ch in listViewDevices.Columns)
            {
                //  ch.Width = Width;
            }
        }

        public void SetFirstSelected()
        {
            if (listViewDevices.Items.Count > 0)
            {
                listViewDevices.Items[0].Selected = true;
                listViewDevices.Select();
            }
        }

        private void listViewDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  CheckBox checkbox = (CheckBox)sender;
        }
        private void listViewDevices_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }
        private void listViewDevices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /*
            CheckBox test = sender as CheckBox;

            for (int i = 0; i < listViewDevices.Items.Count; i++)
            {
                listViewDevices.Items[i].BackColor = DisabledColor;
            }
            */
        }
        private void Bink(object sender, System.EventArgs e)
        {
            /*
            CheckBox test = sender as CheckBox;

            for (int i = 0; i < listViewDevices.Items.Count; i++)
            {
                listViewDevices.Items[i].Checked = test.Checked;
                listViewDevices.Items[i].BackColor = DisabledColor;
            }
            */
        }
        private void listViewDevices_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            /*
            if ((e.ColumnIndex == 0))
            {
                CheckBox cck = new CheckBox();
                // With...
                Text = "";
                Visible = true;
                listViewDevices.SuspendLayout();
                e.DrawBackground();
                cck.BackColor = Form_Main._backColor;
                //cck.UseVisualStyleBackColor = true;

                cck.SetBounds(e.Bounds.X, e.Bounds.Y, cck.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width, cck.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width);
                cck.Size = new Size((cck.GetPreferredSize(new Size((e.Bounds.Width - 1), e.Bounds.Height)).Width + 1), e.Bounds.Height);
                cck.Location = new Point(3, 0);
                listViewDevices.Controls.Add(cck);
                cck.Show();
                cck.BringToFront();
                e.DrawText((TextFormatFlags.VerticalCenter | TextFormatFlags.VerticalCenter));
                cck.Click += new EventHandler(Bink);
                listViewDevices.ResumeLayout(true);
            }
            else
            {
                e.DrawDefault = true;
            }

            var with1 = e.Graphics;
            with1.DrawLines(new Pen(Color.Green), new Point[] { new Point(e.Bounds.Left + e.Bounds.Width, e.Bounds.Top - 1), new Point(e.Bounds.Left + e.Bounds.Width, e.Bounds.Top + e.Bounds.Height) });
            e.DrawText();
            */
        }

        private void listViewDevices_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            listViewDevices.BeginUpdate();

            if (e.ColumnIndex == 4)
            {
                ResizeAutoSizeColumn(listViewDevices, 0);
            }
            if (e.ColumnIndex == 0)
            {
                ResizeAutoSizeColumn(listViewDevices, 4);
            }
          //  ResizeAutoSizeColumn(listViewDevices, 0);
            listViewDevices.EndUpdate();

            //   ResizeColumn();
        }
        /*
        private void ResizeColumn()
        {
            listViewDevices.BeginUpdate();
            listViewDevices.Columns[4].Width = -2; //magic
            listViewDevices.EndUpdate();
        }
        */
        static private void ResizeAutoSizeColumn(ListView listView, int autoSizeColumnIndex)
        {
            // Do some rudimentary (parameter) validation.
            if (listView == null) throw new ArgumentNullException("listView");
            if (listView.View != View.Details || listView.Columns.Count <= 0 || autoSizeColumnIndex < 0) return;
            if (autoSizeColumnIndex >= listView.Columns.Count)
                throw new IndexOutOfRangeException("Parameter autoSizeColumnIndex is outside the range of column indices in the ListView.");

            // Sum up the width of all columns except the auto-resizing one.
            int otherColumnsWidth = 0;
            foreach (ColumnHeader header in listView.Columns)
                if (header.Index != autoSizeColumnIndex)
                    otherColumnsWidth += header.Width;

            // Calculate the (possibly) new width of the auto-resizable column.
            int autoSizeColumnWidth = listView.ClientRectangle.Width - otherColumnsWidth;

            // Finally set the new width of the auto-resizing column, if it has changed.
            if (listView.Columns[autoSizeColumnIndex].Width != autoSizeColumnWidth)
                listView.Columns[autoSizeColumnIndex].Width = autoSizeColumnWidth;
        }

        private void listViewDevices_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {

        //    var with1 = e.Graphics;
          //  with1.DrawLines(new Pen(Color.Green), new Point[] {/*new Point(e.Bounds.Left, e.Bounds.Top - 1),*/new Point(e.Bounds.Left + e.Bounds.Width, e.Bounds.Top - 1), new Point(e.Bounds.Left + e.Bounds.Width, e.Bounds.Top + e.Bounds.Height)/*,new Point(e.Bounds.Left, e.Bounds.Top + e.Bounds.Height)*/});
           // e.DrawText();

        }

        private void DevicesListViewEnableControl_Leave(object sender, EventArgs e)
        {
//            listViewDevices.Enabled = false;
        }

        private void listViewDevices_SizeChanged(object sender, EventArgs e)
        {
         //   ResizeAutoSizeColumn(listViewDevices, 0);
        }

        private void listViewDevices_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (listViewDevices.Columns[TEMP].Width + listViewDevices.Columns[LOAD].Width + listViewDevices.Columns[FAN].Width + listViewDevices.Columns[POWER].Width > 0)
            {
                ConfigManager.GeneralConfig.ColumnENABLED = listViewDevices.Columns[ENABLED].Width;
                ConfigManager.GeneralConfig.ColumnTEMP = listViewDevices.Columns[TEMP].Width;
                ConfigManager.GeneralConfig.ColumnLOAD = listViewDevices.Columns[LOAD].Width;
                ConfigManager.GeneralConfig.ColumnFAN = listViewDevices.Columns[FAN].Width;
                ConfigManager.GeneralConfig.ColumnPOWER = listViewDevices.Columns[POWER].Width;
            }
        }
    }
    public static class ControlExtensions
    {
        public static void DoubleBuffer(this Control control)
        {
            System.Reflection.PropertyInfo dbProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            dbProp.SetValue(control, true, null);
        }
    }
}
