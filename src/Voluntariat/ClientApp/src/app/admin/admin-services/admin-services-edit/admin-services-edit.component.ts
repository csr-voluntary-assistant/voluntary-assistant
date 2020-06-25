import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AdminServicesService } from '../admin-services.service'
import { Service, ServiceStatus, AddedBy } from '../admin-services.models';

@Component({
  selector: 'app-admin-services-edit',
  templateUrl: './admin-services-edit.component.html',
  styleUrls: ['./admin-services-edit.component.css']
})

export class AdminServicesEditComponent implements OnInit {
    id: string;
    status = ServiceStatus;
    addedBy = AddedBy;
    voluntaryService: Service = new Service();

    constructor(private service: AdminServicesService, private actRoute: ActivatedRoute, private router: Router) { }

    ngOnInit(): void {
        this.actRoute.paramMap.subscribe(params => {
            this.id = params.get('id');
        });

        this.service.getByID(this.id).subscribe((data: Service) => {
            this.voluntaryService = data;
        });
    }

    onSubmit() {
        this.service.modifyByID(this.id, this.voluntaryService).subscribe(() => {
            this.router.navigate(['../../list'], { relativeTo: this.actRoute });
        });
    }
}
