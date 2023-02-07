using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    public class GitHelper
    {
        private static GitHelper _githelper= new GitHelper();  
        
        public static GitHelper Instance { get { return _githelper; } }

        private string RepoPath = null;

        public void VerifyRepository(string sourceUrl, string repositoryFolder)
        {
            if (!Repository.IsValid(repositoryFolder))
            {
                //var cloneOpt = new CloneOptions
                //{
                //    CredentialsProvider = (ur, us, ce) => new UsernamePasswordCredentials
                //    {
                //        Username = "MyUser",
                //        Password = ("Some App password from bitbucket")
                //    },
                //    OnTransferProgress = onTransfer,
                //    CertificateCheck = onCertCheck,
                //    OnProgress = onProgress,
                //};

                try
                {
                    StaticObjects.Logger.Info($"Start cloning repository: {sourceUrl}");
                    StaticObjects.Logger.Info($" Destination folder: {repositoryFolder}");
                    RepoPath = Repository.Clone(sourceUrl, repositoryFolder);
                    StaticObjects.Logger.Info($" Cloned to: {RepoPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            else
            {
                RepoPath = Repository.Init(repositoryFolder);
            }
        }

        /// <summary>
        /// Check is a path is a valid repository
        /// </summary>
        /// <param name="respositoryPath"></param>
        /// <returns></returns>
        public bool IsValid(string respositoryPath)
        {
            return Repository.IsValid(respositoryPath);
        }

        public bool Checkout(string repositoryPath, string branchName)
        {
            try
            {
                using (Repository localRepo = new Repository(repositoryPath))
                {
                    Commit localCommit = localRepo.Lookup<Commit>(branchName);
                    Commands.Checkout(localRepo, localCommit);
                }
                return true;
            }
            catch (Exception ex)
            {
                StaticObjects.FireShowExceptionMessage($"Checkout Error, repository= {repositoryPath}, branch= {branchName}: ", ex);
                return false;
            }
        }

        public bool Clone(string sourceUrl, string repositoryPath)
        {
            try
            {
                var cloneOptions = new CloneOptions { BranchName = "master", Checkout = true };
                var cloneResult = Repository.Clone(sourceUrl, repositoryPath);
                return true;
            }
            catch (Exception ex)
            {
                StaticObjects.FireShowExceptionMessage($"Clone Error, repository= {repositoryPath}, sourceUrl= {sourceUrl}: ", ex);
                return false;
            }
        }


    }
}
