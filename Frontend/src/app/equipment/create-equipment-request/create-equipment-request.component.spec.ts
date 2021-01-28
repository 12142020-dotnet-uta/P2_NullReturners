import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEquipmentRequestComponent } from './create-equipment-request.component';

describe('CreateEquipmentRequestComponent', () => {
  let component: CreateEquipmentRequestComponent;
  let fixture: ComponentFixture<CreateEquipmentRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEquipmentRequestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEquipmentRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
