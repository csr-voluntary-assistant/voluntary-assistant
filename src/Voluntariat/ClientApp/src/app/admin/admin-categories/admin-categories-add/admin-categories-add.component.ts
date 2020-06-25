import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AdminCategoriesService } from '../admin-categories.service'
import { Category, CategoryStatus, AddedBy } from '../admin-categories.models';

@Component({
    selector: 'app-admin-categories-add',
    templateUrl: './admin-categories-add.component.html',
    styleUrls: ['./admin-categories-add.component.css']
})

export class AdminCategoriesAddComponent implements OnInit {
    category: Category = new Category();


    constructor(private service: AdminCategoriesService, private actRoute: ActivatedRoute, private router: Router) { }

    ngOnInit(): void { }

    onSubmit() {
        this.category.addedBy = AddedBy.PlatformAdmin;
        this.category.categoryStatus = CategoryStatus.Approved;
        this.category.createdOn = new Date();

        this.service.addCategory(this.category).subscribe(() => {
            this.router.navigate(['../list'], { relativeTo: this.actRoute });
        });     
    }
}
