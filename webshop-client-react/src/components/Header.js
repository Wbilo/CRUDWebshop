import React from 'react';
import {  Link } from 'react-router-dom'; 
import "../css/header.css";   
import "../css/cartImage.css";    
import Categories from "./Categories"; 

 // importer withRouter så jeg kan tilgå props.history osv...
import {withRouter} from 'react-router'; 
   
class Header extends React.Component {

    constructor(props) {
        super(props);
        const defaultDropdownName = "All Brands";
        this.state = {
            defaultCategoryName: defaultDropdownName,
            categoryFilterValue: defaultDropdownName,
            allCategories: true, 
            shopName: "Shoe-Shop",
            frontPageEndPoint: "/",
            categoryEndpoint: "/Brand/?"
        };
        this.updateCategoryName = this.updateCategoryName.bind(this);
    }
  
   updateCategoryName (categoryName) {
        this.setState({ categoryFilterValue: categoryName, allCategories: categoryName === this.state.defaultCategoryName ? true : false });
        if(categoryName === this.state.defaultCategoryName)
        {
            this.props.history.push(
                this.state.frontPageEndPoint 
            );  

        }
        else {
            this.props.history.push(
                this.state.categoryEndpoint +
                categoryName  
            );  
 
        } 
   } 

    render() {
        return ( 
            <div className="header"> 
                <Link to={this.state.frontPageEndPoint}><p className="logo">{this.state.shopName}</p></Link>   
                <Categories
                currentCategory={this.state.categoryFilterValue} 
                onChange={this.updateCategoryName}
                allCategories={this.state.allCategories}     
                />      
                <div className="header-right">  
                    <Link to={this.state.frontPageEndPoint}><p>Home</p></Link>
                    {this.props.children}             
               </div>  
            </div>      
        )  

    }
      
 }
// exporter withRouter så jeg kan tilgå props.history osv...
export default withRouter(Header);