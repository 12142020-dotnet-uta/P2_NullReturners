import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEquipmentRequestComponent } from './edit-equipment-request.component';

describe('EditEquipmentRequestComponent', () => {
  let component: EditEquipmentRequestComponent;
  let fixture: ComponentFixture<EditEquipmentRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditEquipmentRequestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEquipmentRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
