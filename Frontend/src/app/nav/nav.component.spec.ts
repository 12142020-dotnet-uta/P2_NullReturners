import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';

import { NavComponent } from './nav.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { By } from '@angular/platform-browser';
import {Location} from '@angular/common';
import { Component } from '@angular/core';

describe('NavComponent', () => {
  let component: NavComponent;
  let fixture: ComponentFixture<NavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule
        .withRoutes([{path: '', component: DummyComponent}, {path: 'plays', component: DummyComponent}])],
      declarations: [ NavComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show unordered list items', () => {
    const unorderedList = fixture.debugElement.queryAll(By.css('ul'));
    expect(unorderedList.length).toBe(1);
  });

  it('should show unordered list items', () => {
    const listItems = fixture.debugElement.queryAll(By.css('li'));
    expect(listItems.length).toBe(7);
  });

  it('should navigate to home page', () => {
    const location = TestBed.inject(Location)
    expect(location.path()).toBe('');
  });

  it('should submit username and password to login', () => {
    const listOfInputs = fixture.debugElement.queryAll(By.css('input'));
    const userName: HTMLInputElement = listOfInputs[0].nativeElement;
    const password: HTMLInputElement = listOfInputs[1].nativeElement;
    fixture.detectChanges();

    userName.value = "travis";
    password.value = "travis123";

    userName.dispatchEvent(new Event('input'));
    password.dispatchEvent(new Event('input'));

    expect(userName.value).toBe('travis');
    expect(password.value).toBe('travis123');

    spyOn(component, "login");
    const allButtons = fixture.debugElement.queryAll(By.css('button'));
    const submit: HTMLLinkElement = allButtons[1].nativeElement;
    submit.click();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(component.login()).toHaveBeenCalled();
    }); 
  });

  it('should call logout', () => {
    spyOn(component, "logout");
    const listOfLinks = fixture.debugElement.queryAll(By.css('a'));
    const logout: HTMLInputElement = listOfLinks[7].nativeElement;
    logout.click();
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      expect(component.logout()).toHaveBeenCalled();
    });
  });

});


@Component({template: ''})
class DummyComponent {

}