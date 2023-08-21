using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomProfileAPI.Data;
using RandomProfileAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RandomProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImagesController : ControllerBase
    {
        private readonly RandomProfileContext _context;

        public ProfileImagesController(RandomProfileContext context)
        {
            _context = context;
        }

        // GET: api/ProfileImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileImageDTO>>> GetProfileImages()
        {
            return await _context.ProfileImages
                .Select(pi => new ProfileImageDTO 
                {
                    ProfileImageID = pi.ProfileImageID,
                    Name = pi.Name,
                    ProfileImageBytes = pi.ProfileImageBytes,
                    Description = pi.Description
                }).ToListAsync();
        }

        // GET: api/ProfileImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileImage>> GetProfileImage(int id)
        {
            var ProfileImage = await _context.ProfileImages.FindAsync(id);

            if (ProfileImage == null)
            {
                return NotFound();
            }

            return ProfileImage;
        }

        // GET: api/ProfileImages/inc/5
        [HttpGet("inc/{id}")]
        public async Task<ActionResult<ProfileImageDTO>> GetProfileImageInc(int id)
        {
            var ProfileImageDTO = await _context.ProfileImages
                .Include(d => d.Profiles)
                .Select(d => new ProfileImageDTO
                {
                    ProfileImageID = d.ProfileImageID,
                    Name = d.Name,
                    ProfileImageBytes = d.ProfileImageBytes,
                    Description = d.Description,
                    Profiles = d.Profiles.Select(dProfile => new ProfileDTO
                    {
                        ID = dProfile.ID,
                        FirstName = dProfile.FirstName,
                        MiddleName = dProfile.MiddleName,
                        LastName = dProfile.LastName,
                        NickName = dProfile.NickName,
                        Bio = dProfile.Bio,
                        DOB = dProfile.DOB,
                        ProfileImageID = dProfile.ProfileImageID
                    }).ToList()
                })
                .FirstOrDefaultAsync(p => p.ProfileImageID == id);

            if (ProfileImageDTO == null)
            {
                return NotFound(new { message = "Error: ProfileImage not found." });
            }

            return ProfileImageDTO;
        }

        // PUT: api/ProfileImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfileImage(int id, ProfileImageDTO ProfileImageDTO)
        {
            if (id != ProfileImageDTO.ProfileImageID)
            {
                return BadRequest(new { message = "Error: ID does not match ProfileImage"});
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Get the record you want to update
            var ProfileImageToUpdate = await _context.ProfileImages.FindAsync(id);

            //Check that you got it
            if (ProfileImageToUpdate == null)
            {
                return NotFound(new { message = "Error: Profile record not found." });
            }

            // Update the properties of the entity object from the DTO object
            ProfileImageToUpdate.ProfileImageID = ProfileImageDTO.ProfileImageID;
            ProfileImageToUpdate.Name = ProfileImageDTO.Name;
            ProfileImageToUpdate.ProfileImageBytes = ProfileImageDTO.ProfileImageBytes;
            ProfileImageToUpdate.Description = ProfileImageDTO.Description;
      

            _context.Entry(ProfileImageDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileImageExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: Profile Image has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Profile Image has been updated by another user.  Back out and try editing the record again." });
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }
        }

        // POST: api/ProfileImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfileImage>> PostProfileImage(ProfileImageDTO ProfileImageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProfileImage ProfileImage = new ProfileImage
            {
                Name = ProfileImageDTO.Name,
                ProfileImageBytes = ProfileImageDTO.ProfileImageBytes,
                Description = ProfileImageDTO.Description
            };

            try
            {
                _context.ProfileImages.Add(ProfileImage);
                await _context.SaveChangesAsync();
                //Assign Database Generated values back into the DTO
                ProfileImageDTO.ProfileImageID = ProfileImage.ProfileImageID;
                return CreatedAtAction(nameof(GetProfileImage), new { id = ProfileImage.ProfileImageID }, ProfileImageDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }
        }

        // DELETE: api/ProfileImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfileImage>> DeleteProfileImage(int id)
        {
            var ProfileImage = await _context.ProfileImages.FindAsync(id);
            if (ProfileImage == null)
            {
                return NotFound(new { message = "Delete Error: Profile Image has already been removed." });
            }
            try
            {
                _context.ProfileImages.Remove(ProfileImage);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    return BadRequest(new { message = "Delete Error: Remember, you cannot delete a Profile Image that has Profiles assigned." });
                }
                else
                {
                    return BadRequest(new { message = "Delete Error: Unable to delete Profile Image. Try again, and if the problem persists see your system administrator." });
                }
            }
        }

        private bool ProfileImageExists(int id)
        {
            return _context.ProfileImages.Any(e => e.ProfileImageID == id);
        }
    }
}
