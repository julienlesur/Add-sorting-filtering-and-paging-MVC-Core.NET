﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Web.Models.ViewModels;
using Recruiting.Infrastructures.ActionFilters;
using Microsoft.Extensions.Options;
using Recruiting.Infrastructures.Configurations;
using Recruiting.Web.Infrastructures;

namespace Recruiting.Web.Controllers
{
    [JobReference]
    public class ApplicantsController : PagingSortingSearchingControllerBase
    {
        private readonly IApplicantService _applicantService;

        public override string _sortOrder => SortOrder ?? Applicant._DefaultSort;
        public ApplicantsController(IApplicantService applicantService,
                                        IOptions<GridConfiguration> gridOptions)
            : base(gridOptions)
        {
            _applicantService = applicantService;
        }

        [PagingSortingSearching]
        public async Task<IActionResult> List(string jobReference)
        {
            (IEnumerable<Applicant> applicants, int numberOfItems) = await _applicantService.GetApplicantList(jobReference, _searchText, _sortOrder, _indexPage, _gridOptions.ItemsPerPage);

            return View(new ApplicantList {
                Applicants = applicants,
                ListTitle = String.IsNullOrEmpty(_searchText) ? "Current applicants" : "Filtered applicants",
                JobColumnTitle = "Last application",
                NumberOfItems = numberOfItems
            }); ;
        }


        public async Task<IActionResult> Details(int id, string jobReference)
        {
            Applicant applicant = await _applicantService.FindByIdAsync(id);
            if (Applicant.IsEmpty(applicant))
            {
                return RedirectToAction(nameof(ApplicantNotFound));
            }
            return View(new ApplicantDetails { Applicant = applicant, Message = (TempData["Message"] ?? "").ToString() });
        }


        public IActionResult Add(string jobReference)
        {
            return View("Edit", Applicant._EmptyApplicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("FirstName, LastName, Email, Adress1, Adress2, ZipCode, City, Country")] Applicant applicant, string jobReference)
        {
            if (ModelState.IsValid)
            {
                var newApplicant = await _applicantService.AddAsync(applicant, jobReference);
                TempData["Message"] = "The applicant has been succesfully added";
                return RedirectToAction(nameof(Details), new { id = newApplicant.ApplicantId, jobReference = jobReference });
            }
            return View("Edit", new { id = applicant.ApplicantId, jobReference = jobReference });
        }

        public async Task<IActionResult> Edit(int id, string jobReference)
        {
            var applicant = await _applicantService.FindByIdAsync(id);
            if (Applicant.IsEmpty(applicant))
            {
                return RedirectToAction(nameof(ApplicantNotFound));
            }

            return View(applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantId, FirstName, LastName, Email, Adress1, Adress2, ZipCode, City, Country")] Applicant applicant, string jobReference)
        {
            if (ModelState.IsValid)
            {
                var updatedApplicant = await _applicantService.UpdateAsync(applicant);
                TempData["Message"] = "The applicant has been succesfully saved";
                return RedirectToAction(nameof(Details), new { id = updatedApplicant.ApplicantId, jobReference = jobReference });
            }
            return View(applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedApplicant = await _applicantService.DeleteAsync(id);
            if (Applicant.IsEmpty(deletedApplicant))
            {
                return RedirectToAction(nameof(ApplicantNotFound));
            }
            return RedirectToAction(nameof(List));
        }

        public IActionResult ApplicantNotFound()
        {
            return View();
        }
    }
}
