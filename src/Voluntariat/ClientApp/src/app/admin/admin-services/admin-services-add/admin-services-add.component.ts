import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AdminServicesService } from '../admin-services.service'
import { Service, ServiceStatus, AddedBy } from '../admin-services.models';

@Component({
  selector: 'app-admin-services-add',
  templateUrl: './admin-services-add.component.html',
  styleUrls: ['./admin-services-add.component.css']
})

export class AdminServicesAddComponent implements OnInit {
    voluntaryService: Service = new Service();


    constructor(private service: AdminServicesService, private actRoute: ActivatedRoute, private router: Router) { }

    ngOnInit(): void { }

    onSubmit() {
        this.voluntaryService.addedBy = AddedBy.PlatformAdmin;
        this.voluntaryService.serviceStatus = ServiceStatus.Approved;
        this.voluntaryService.createdOn = new Date();

        this.service.addService(this.voluntaryService).subscribe(() => {
            this.router.navigate(['../list'], { relativeTo: this.actRoute });
        });
    }
}
