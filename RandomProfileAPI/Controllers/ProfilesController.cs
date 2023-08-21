using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomProfileAPI.Data;
using RandomProfileAPI.Models;

namespace RandomProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly RandomProfileContext _context;

        public ProfilesController(RandomProfileContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetProfiles()
        {
            return await _context.Profiles
                .Include(p => p.ProfileImage)
                .Select(p => new ProfileDTO
                {
                    ID = p.ID,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    Bio = p.Bio,
                    NickName = p.NickName,
                    DOB = p.DOB,
                    ProfileImageID = p.ProfileImageID,
                    ProfileImage = new ProfileImageDTO 
                    {
                        ProfileImageID = p.ProfileImage.ProfileImageID,
                        Name = p.ProfileImage.Name,
                        ProfileImageBytes = p.ProfileImage.ProfileImageBytes,
                        Description = p.ProfileImage.Description,
                    }
                })
                .ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDTO>> GetProfile(int id)
        {
            var profile = await _context.Profiles
                .Include(p => p.ProfileImage)
                .Select(p => new ProfileDTO
                {
                    ID = p.ID,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    Bio = p.Bio,
                    NickName = p.NickName,
                    DOB = p.DOB,
                    ProfileImageID = p.ProfileImageID,
                    ProfileImage = new ProfileImageDTO
                    {
                        ProfileImageID = p.ProfileImage.ProfileImageID,
                        Name = p.ProfileImage.Name,
                        ProfileImageBytes = p.ProfileImage.ProfileImageBytes,
                        Description = p.ProfileImage.Description,
                    }
                })
                .FirstOrDefaultAsync(p => p.ID == id);

            if (profile == null)
            {
                return NotFound(new { message = "Error: Profile record not found."});
            }

            return profile;
        }

        // GET: api/ProfilesByProfileImage
        [HttpGet("ByProfileImage/{id}")]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetProfilesByProfileImage(int id)
        {
            var profileDTOs = await _context.Profiles
                .Include(e => e.ProfileImage)
                .Select(p => new ProfileDTO
                {
                    ID = p.ID,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    Bio = p.Bio,
                    NickName = p.NickName,
                    DOB = p.DOB,
                    ProfileImageID = p.ProfileImageID,
                    ProfileImage = new ProfileImageDTO
                    {
                        ProfileImageID = p.ProfileImage.ProfileImageID,
                        Name = p.ProfileImage.Name,
                        ProfileImageBytes = p.ProfileImage.ProfileImageBytes,
                        Description = p.ProfileImage.Description,
                    }
                })
                .Where(e => e.ProfileImageID == id)
                .ToListAsync();

            if (profileDTOs.Count() > 0)
            {
                return profileDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No Profile records for that Profile Image." });
            }
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, ProfileDTO profileDTO)
        {
            if (id != profileDTO.ID)
            {
                return BadRequest(new { message = "Error: ID does not match profile" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Get the record you want to update
            var profileToUpdate = await _context.Profiles.FindAsync(id);

            //Check that you got it
            if (profileToUpdate == null)
            {
                return NotFound(new { message = "Error: Profile record not found." });
            }


            //Update the properties of the entity object from the DTO object
            profileToUpdate.ID = profileDTO.ID;
            profileToUpdate.FirstName = profileDTO.FirstName;
            profileToUpdate.MiddleName = profileDTO.MiddleName;
            profileToUpdate.LastName = profileDTO.LastName;
            profileToUpdate.Bio = profileDTO.Bio;
            profileToUpdate.NickName = profileDTO.NickName;
            profileToUpdate.DOB = profileDTO.DOB;
            profileToUpdate.ProfileImageID = profileDTO.ProfileImageID;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: Profile has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Profile has been updated by another user.  Back out and try editing the record again." });
                }
            }
            catch (DbUpdateException)
            {

                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }

        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(ProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Profile profile = new Profile
            {
                ID = profileDTO.ID,
                FirstName = profileDTO.FirstName,
                MiddleName = profileDTO.MiddleName,
                LastName = profileDTO.LastName,
                Bio = profileDTO.Bio,
                NickName = profileDTO.NickName,
                DOB = profileDTO.DOB,
                ProfileImageID = profileDTO.ProfileImageID
            };

            try
            {
                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();

                //Assign Database Generated values back into the DTO
                profileDTO.ID = profile.ID;

                return CreatedAtAction(nameof(GetProfile), new { id = profile.ID }, profileDTO);
            }
            catch (DbUpdateException)
            {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound(new { message = "Delete Error: Profile has already been removed." });
            }
            try
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Delete Error: Unable to delete Profile." });
            }
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ID == id);
        }
    }
}
