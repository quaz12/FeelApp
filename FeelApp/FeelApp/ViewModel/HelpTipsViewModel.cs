using FeelApp.Model;
using FeelApp.ViewModel.Base;
using FeelApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeelApp.ViewModel
{
    public class HelpTipsViewModel :BaseViewModel
    {
        public HelpTipsViewModel(Page page)
        {
            this.Page = page;
            _helpTipsList = new ObservableCollection<HelpTips>();
            InitList();
        }

        private void InitList()
        {
            
            var list = new ObservableCollection<HelpTips>();
            list.Add(new HelpTips { Id = 0, Tip = "Assess the situation: Crawl to the door on hands and knees, so you can breathe the fresher air near the floor. Touch it with your hand—only briefly, because it may be very hot. If the door is hot, do not open it.", Image="Tip1.png"});
            list.Add(new HelpTips { Id = 1, Tip = "Notify authorities: Instead, call the admin, give your name and room number, and report the fire. Then, get an outside line and dial 9-1-1 to report the fire directly to authorities. This call may save your life and may be the first alert the fire department receives.", Image = "Tip2.png" });           
            list.Add(new HelpTips { Id = 2, Tip = "Seal your room against entering smoke: Turn off the ventilation system. Block the ventilation duct and the spaces under doors. If there is any smoke already in your room, clear it by opening the windows.", Image = "Tip3.png" });
            list.Add(new HelpTips { Id = 3, Tip = "Protect your lungs: If it's still smoky in your room, cover your nose and mouth. Breathe only through your nose. Grip part of the towel with your lips and teeth. It can help remind you not to breathe through your mouth. ", Image = "Tip4.png" });
            list.Add(new HelpTips { Id = 4, Tip = "Clear flammable debris from the window: Rip off the curtains and anything else that could burn. Don't break the glass. You may need to close it against smoke entering from outside. But, as long as the air outside is fresh, open the window a crack and breathe it in.", Image = "Tip5.png" });
            list.Add(new HelpTips { Id = 5, Tip = "Keep fighting until help arrives: Many people in fires have jumped to their deaths, not knowing that help was on the way. If you have to jump from the window, push out and away from the building to avoid hitting ledges on the way down.", Image = "Tip6.png" });
            HelpTipsList = list;
        }

        private ObservableCollection<HelpTips> _helpTipsList;
        public ObservableCollection<HelpTips> HelpTipsList
        {
            get { return _helpTipsList; }
            set { SetProperty(ref _helpTipsList, value, "HelpTipsList"); }
        }
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand = _backCommand ?? new DelegateCommand(async (obj) => await BackEvent()); }
        }

        private async Task BackEvent()
        {
            await Page.Navigation.PopAsync();
        }
    }
}
