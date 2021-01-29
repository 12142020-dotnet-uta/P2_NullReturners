import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlayerComponent } from './create-player.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';

import { By } from '@angular/platform-browser';
import { Component } from '@angular/core';
import {Location} from '@angular/common';
import { Router } from '@angular/router';

describe('CreatePlayerComponent', () => {
  let component: CreatePlayerComponent;
  let fixture: ComponentFixture<CreatePlayerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule.withRoutes([{path: 'players', component: DummyComponent}])],
      declarations: [ CreatePlayerComponent ]
    })
    .compileComponents(); 
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have an h3 tag', () => {
    const fixture = TestBed.createComponent(CreatePlayerComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('div').textContent).toContain('Add New User');
  });

  it('should have form inputs', () => {
    const allInputs = fixture.debugElement.queryAll(By.css('input'));
    const userNameInput: HTMLInputElement = allInputs[0].nativeElement;
    expect(userNameInput.name).toBe('Username');
    const passwordInput: HTMLInputElement = allInputs[1].nativeElement;
    expect(passwordInput.name).toBe('Password');
    const fullNameInput: HTMLInputElement = allInputs[2].nativeElement;
    expect(fullNameInput.name).toBe('FullName');
    const phoneNumberInput: HTMLInputElement = allInputs[3].nativeElement;
    expect(phoneNumberInput.name).toBe('PhoneNumber');
    const emailInput: HTMLInputElement = allInputs[4].nativeElement;
    expect(emailInput.name).toBe('Email');
  });

  it('should have form submit button', () => {
    const allButtons = fixture.debugElement.queryAll(By.css('button'));
    const formSubmitButton: HTMLInputElement = allButtons[0].nativeElement;
    expect(formSubmitButton.textContent).toBe('Create');
  });

  it('should navigate to create player', () => {
    const location = TestBed.inject(Location)
    expect(location.path()).toBe('');
  });

  it('should navigate to players when clicking on link', () => {
    // const router = TestBed.inject(Router)
    // spyOn(router, 'navigateByUrl');
    const location = TestBed.inject(Location)
    const allLinks = fixture.debugElement.queryAll(By.css('a'));
    const playerLink: HTMLLinkElement = allLinks[0].nativeElement;
    playerLink.click();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(location.path()).toBe('/players');
    }); 
    // fixture.whenStable().then(() => {
    //   expect(router.navigateByUrl).toHaveBeenCalledWith(router.createUrlTree(['/players']), 
    //   { skipLocationChange: false, replaceUrl: false});
    // });    
  });
});

@Component({template: ''})
class DummyComponent {

}