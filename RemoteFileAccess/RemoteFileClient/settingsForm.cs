using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteFileClient
{
    public partial class settingsForm : Form
    {
        // Используем словарь для отслеживания изменений относительно начального состояния отмеченных свойств файла/папки
        public Dictionary<string, CheckState> initState = new Dictionary<string, CheckState>();
        // Список чекбоксов
        private List<CheckBox> boxes = new List<CheckBox>();
        // Объект главной формы
        clientForm form;

        public settingsForm(clientForm form)
        {
            InitializeComponent();
            // Получаем объект главной формы
            this.form = form;
            // Собираем список всех чекбоксов
            boxes.Add(cb_directory);
            boxes.Add(cb_archive);
            boxes.Add(cb_compressed);
            boxes.Add(cb_encrypted);
            boxes.Add(cb_normal);
            boxes.Add(cb_hidden);
            boxes.Add(cb_system);
            boxes.Add(cb_readonly);
        }
        // Обработчик изменения состояния чекбокса
        public void cb_CheckStateChanged(object sender, EventArgs e)
        {
            // Выставляем маркер применять изменения или нет 
            form.apply = false;// не применять
            // Получаем идентификатор чекбокса сгенерировавшего событие изминения состояния (снята галочка или отмечена)
            CheckBox cb = sender as CheckBox;
            // Пробегаемся по всем чекбоксам формы
            foreach (var b in boxes)
            {
                // Проверяем состояние чекбокса с именем [Name], если текущее состояние не совподает с тем которое было задано при создании
                if (initState[b.Name] != b.CheckState)
                {
                    // тогда выставляем маркер в "применить изменения"
                    form.apply = true;
                    // выходим из перечисления если хотя бы один чекбокс изменил состояние
                    break;
                }
            }
            // Включаем или выключаем кнопку применения изменения для выбранного файла в зависимости от маркер "apply"
            btn_apply.Enabled = form.apply;
        }
        
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form.apply = false;
            // Закрываем форму
            Close();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            form.apply = true;
            // Собираем все атрибуты которые были отмечены галочками
            form.fileInfo.Attributes = System.IO.FileAttributes.Normal;
            if (cb_directory.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Directory;
            if (cb_archive.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Archive;
            if (cb_compressed.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Compressed;
            if (cb_encrypted.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Encrypted;
            if (cb_hidden.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Hidden;
            if (cb_system.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.System;
            if (cb_readonly.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.ReadOnly;
            if (cb_normal.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Normal;
            // Закрываем форму
            Close();
        }
    }
}
