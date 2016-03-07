﻿using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Shell
{
    public class QuestionnaireShellViewModel : IQuestionnaireShellViewModel, INavigationAware
    {
        public ICommand AdvanceNextCommand { get; private set; }
        
        public ICommand AdvancePreviousCommand { get; private set; }

        private int currentQuestionIndex = 0;

        private readonly IRegionManager regionManager;

        public QuestionnaireShellViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.AdvanceNextCommand = new DelegateCommand(AdvanceNextCommand_Execute);
            this.AdvancePreviousCommand = new DelegateCommand(AdvancePreviousCommand_Execute);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question1);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {          
        }

        private void AdvanceNextCommand_Execute()
        {
            switch (currentQuestionIndex)
            {
                case 0:
                    currentQuestionIndex++;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question2);
                    break;
                case 1:
                    currentQuestionIndex++;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question3);
                    break;
                case 2:
                    currentQuestionIndex = 0;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question1);
                    break;
                default:
                    regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Landing);
                    break;
            }
        }

        private void AdvancePreviousCommand_Execute()
        {
            switch (currentQuestionIndex)
            {
                case 0:
                    currentQuestionIndex = 2;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question3);
                    break;
                case 1:
                    currentQuestionIndex--;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question1);
                    break;
                case 2:
                    currentQuestionIndex--;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question2);
                    break;
                default:
                    regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Landing);
                    break;
            }
        }
    }
}
