import React from "react";
import CategoryCard from "./CategoryCard";  
class CategoriesList extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      condition: false 
    };
     

    this.handleClick = this.handleClick.bind(this); 
  }
   // Bliver kaldt når dette component bliver indsat ind i dom træet for første gang   
  componentDidMount() {
    // tilføjer event listener som kalder this.handlClick når den bliver triggered
    window.addEventListener('click', this.handleClick); 
  } 
 
  // Håndtere klik af dropdown menu 
  handleClick(event) {  
    this.setState({
      // Hvis man trykker på dropbtn button, så sætter vi
      // condition til true, hvilket gør at div med id'et Categ
      // får tilføjet klassen show og kategorier bliver vist
      // hvis ikke, så har vi trykket et andet sted og det modsatte sker  
      condition: event.target.matches('.dropbtn') 
      ? true : false  
    });   
  } 
  // Bliver kaldt når state ændrer sig. 
  render() { 

    return ( 
      <div className="dropdown">
        <button onClick={this.handleClick} className="dropbtn">{this.props.currentCategory}</button> 
        <div 
          ref = "CategoryMenu" 
          id="Categ"
          className={this.state.condition ? "Categories show" : "Categories"}
          
        >
         {this.props.allCategories ? ""  : <CategoryCard
              key={0}
              categId={0}
              name={this.props.placeHolder} 
              onClick={this.props.onChange}
            />}    
          {this.props.categories.map(element => (
            <CategoryCard
              key={element.brand_id}
              categId={element.brand_id}
              name={element.brand_name} 
              onClick={this.props.onChange}
            />
          ))}
        </div> 
      </div >
    );
  } 
}
export default CategoriesList;     
    