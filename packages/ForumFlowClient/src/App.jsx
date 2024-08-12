import "./App.css";
import React, { useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import UserHomePage from "./pages/UserHomePage";
import AddPresentation from "./components/AddPresentation";
import Presentation from "./components/Presentation";
import Comment from "./components/comment";
import AddComment from "./components/AddComment";
import LandingPage from "./pages/LandingPage";
import LoginPage from "./pages/LoginPage";
import CreateAccountPage from "./pages/CreateAccountPage";
import Forum from "./pages/Forum";
import FaqPage from "./pages/FaqPage";
import "./index.css";

//a lot of this is legacy code that is now in Forum.jsx, just keeping it here for reference

function App() {
  const [comments, setComments] = useState([]);
  const [buttonClicked, setButtonClicked] = useState(false);
  const [presentation, setPresentation] = useState(null);
  const [landingPage, setLandingPage] = useState(true);

  const handleAddComment = (text, parentId = null) => {
    const newComment = {
      id: comments.length + 1, // Simple unique ID generation
      text,
      parentId,
      upvotes: 0,
      downvotes: 0,
    };
    setComments([...comments, newComment]);
  };

  const handleAddPresentation = () => {
    if (!buttonClicked) {
      setButtonClicked(true);
    } else {
      setButtonClicked(false);
    }
  };
  const handleNewPresentation = (addPresentation) => {
    setPresentation(addPresentation);
    setButtonClicked(false);
  };

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LandingPage />}></Route>
        <Route path="user">
          <Route path="login" element={<LoginPage />} />
          <Route path="create" element={<CreateAccountPage />} />
          <Route path="home" element={<UserHomePage />} />
          <Route path="add-presentation" element={<AddPresentation />} />
          <Route path="forum" element={<Forum />} />
          <Route path="faq" element={<FaqPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
export default App;

// <>
//   {landingPage ? (
//     <LandingPage />
//   ) : (
//     <>
//       {!buttonClicked && (
//         <button className="btn btn-primary mb-3" onClick={handleAddPresentation}>
//           Add Presentation
//         </button>
//       )}
//       {buttonClicked ? (
//         <NewPresentation onSubmit={handleNewPresentation} />
//       ) : (
//         <>
//           {presentation ? (
//             <Presentation
//               author={presentation.author}
//               text={presentation.text}
//               title={presentation.title}
//             />
//           ) : (
//             <div className='container'>
//               <Presentation
//                 author="Presenter's name"
//                 text="An faq/forum web application where people can create a forum post, share a
//                 link via a qr code, and people can comment and upvote on the forum post.
//                 Our main application of this would allow people/ presenters to provide information to users/ helping informative."
//                 title="sample title"
//               />
//             </div>
//           )}
//           <AddComment onSubmit={handleAddComment} parentId={null} />
//           {comments.map((comment) => (
//             <div className='container' key={comment.id}>
//               <Comment
//                 text={comment.text}
//                 upvotes={comment.upvotes}
//                 downvotes={comment.downvotes}
//                 id={comment.id}
//                 parentId={comment.parentId}
//               />
//               <AddComment onSubmit={handleAddComment} parentId={comment.id} />
//             </div>
//           ))}
//         </>
//       )}
//     </>
//   )}
// </>
