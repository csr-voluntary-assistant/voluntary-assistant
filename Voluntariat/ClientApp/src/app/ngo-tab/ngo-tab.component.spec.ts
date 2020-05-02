import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgoTabComponent } from './ngo-tab.component';

describe('NgoTabComponent', () => {
  let component: NgoTabComponent;
  let fixture: ComponentFixture<NgoTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NgoTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NgoTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
