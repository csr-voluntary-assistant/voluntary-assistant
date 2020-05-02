import { Component, OnInit } from '@angular/core';




export interface NgoList {
  name: string;
  status: string;
  nrCrt: number;
  createdBy: string;
}

const ELEMENT_DATA: NgoList[] = [
  {nrCrt: 1, name: 'Abc', status: 'OK', createdBy : 'user1@abc.com'},
  {nrCrt: 2, name: 'Def', status: 'OK', createdBy : 'user1@abc.com'},
  {nrCrt: 3, name: 'Ghi', status: 'Verification Ongoing',createdBy : 'user2@abc.com'},
  {nrCrt: 4, name: 'Jkl', status: 'Verification Ongoing', createdBy : 'user2@abc.com'},
  {nrCrt: 5, name: 'Mno', status: 'Details needed', createdBy : 'user2@abc.com'},
  {nrCrt: 6, name: 'Pqr', status: 'OK', createdBy : 'user3@abc.com'},
  {nrCrt: 7, name: 'Stu', status: 'OK', createdBy : 'user1@abc.com'},
  {nrCrt: 8, name: 'Vwx', status: 'OK', createdBy : 'user3@abc.com'},
  {nrCrt: 9, name: 'Yzz', status: 'OK', createdBy : 'user3@abc.com'},
  {nrCrt: 10, name: 'Zbg', status: 'OK', createdBy : 'user1@abc.com'},
];



@Component({
  selector: 'app-ngo-tab',
  templateUrl: './ngo-tab.component.html',
  styleUrls: ['./ngo-tab.component.css']
})


export class NgoTabComponent implements OnInit {
  displayedColumns: string[] = ['nrCrt', 'name', 'status', 'createdBy'];
  dataSource = ELEMENT_DATA;
  constructor() {}

  ngOnInit() {
  }

}
