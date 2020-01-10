import React from 'react';  
import CategoriesList from "./CategoriesList"; 
import { WebshopEndpoint } from "../constants"; 
//import { BrandsEndpoint } from "../constants";   

class Categories extends React.Component { 
    constructor(props) {
      super(props); 
  
      this.state = {
        categories: [],
        placeHolder: "All Brands"
      };
    } 
      // Bliver kaldt når dette component bliver indsat ind i dom træet for første gang   
    componentDidMount() {
      // Henter info om brands 
        const url = WebshopEndpoint + "/Webshop/GetAllBrandsInfo";  
      fetch(url)
        .then(response => response.json())
        .then(json => this.setState({ categories: json }))
        .catch(() => {
          console.log("An error occured");
        });
    }
  
    // Bliver kaldt når this.state ændrer sig. 
    render() {   

      return (
             <CategoriesList categories={this.state.categories} 
             currentCategory={this.props.currentCategory}
             onChange={this.props.onChange}
             allCategories={this.props.allCategories} 
             placeHolder={this.state.placeHolder}
             />  
      );      
    } 
  }
  
  export default Categories;  